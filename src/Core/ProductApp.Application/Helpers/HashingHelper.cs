using System.Security.Cryptography;
using System.Text;

namespace ProductApp.Application.Helpers;

public class HashingHelper
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(password));
        using HMACSHA512 hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(password));
        ArgumentException.ThrowIfNullOrEmpty(nameof(password));
        ArgumentException.ThrowIfNullOrEmpty(nameof(password));
        using HMACSHA512 hmac = new HMACSHA512(passwordSalt);
        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
}
