using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using ProductApp.Application;
using ProductApp.Application.Common;
using ProductApp.Persistence;
using ProductApp.WebApi.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
AppSettings? appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

string host = appSettings?.RabbitMq?.HostName ?? string.Empty;
string username = appSettings?.RabbitMq?.UserName ?? string.Empty;
string password = appSettings?.RabbitMq?.Password ?? string.Empty;
string key = appSettings?.JwtSettings?.SecretKey ?? string.Empty;
string issuer = appSettings?.JwtSettings?.Issuer ?? string.Empty;
string audience = appSettings?.JwtSettings?.Audience ?? string.Empty;


builder.Host.UseSerilog((_, loggerConfiguration) => loggerConfiguration.WriteTo.Console(formatProvider: null).ReadFrom.Configuration(builder.Configuration));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(host, h =>
        {
            h.Username(username);
            h.Password(password);
        });
    });
});

builder.Services.AddLogging(configure =>
{
    configure.AddConsole();
    configure.SetMinimumLevel(LogLevel.Trace);
});

builder.Services
    .AddApplicationRegistration()
    .AddPersistenceRegistration(builder.Configuration);

// Add services to the container.

builder.Services
    .AddControllers();
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
//});

builder.Services.AddOpenApi();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Token doğrulama hatası: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"Token başarılı: {context.Principal?.Identity?.Name}");
                return Task.CompletedTask;
            },
            OnMessageReceived = context =>
            {
                var token = context.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(token))
                {
                    //context.Token = token;
                }
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine("JWT token challenge oluştu (401 Unauthorized).");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProductApp API",
        Version = "v1",
        Description = "JWT"
    });

    // JWT Bearer Security Definition
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT token'ı Bearer eklemeden '{token}' formatında girin."
    });
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; 
});

var app = builder.Build();

app.UseResponseCompression();
app.UseErrorHandler();
app.UseSerilogRequestLogging();
app.UseLoggingHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductApp API v1");
        options.RoutePrefix = string.Empty; 
    });
}

app.Run();
