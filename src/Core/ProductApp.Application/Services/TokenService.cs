using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Services;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Services;

public class TokenService : ITokenService
{
    private readonly AppSettings _settings;

    public TokenService(IOptions<AppSettings> options)
    {
        _settings = options.Value;
    }

    public string GenerateAccessToken(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings?.JwtSettings?.SecretKey ?? string.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        if (user.UserOperationClaims != null && user.UserOperationClaims.Any())
        {
            claims.AddRange(user.UserOperationClaims.Select(r => new Claim(ClaimTypes.Role, r.OperationClaim?.Name ?? string.Empty)));
        }

        var token = new JwtSecurityToken(
            issuer: _settings?.JwtSettings?.Issuer ?? string.Empty,
            audience: _settings?.JwtSettings?.Audience ?? string.Empty,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings?.JwtSettings?.TokenExpirationMinutes ?? 0),
            signingCredentials: creds ,
            notBefore: DateTime.UtcNow
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public RefreshToken GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomBytes),
            Expires = DateTime.UtcNow.AddMinutes(_settings?.JwtSettings?.RefreshTokenExpirationMinutes ?? 0)
        };
    }
}
