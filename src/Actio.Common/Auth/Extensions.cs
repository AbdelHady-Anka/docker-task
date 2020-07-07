using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Actio.Common.Auth
{
    public static class Extensions
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("jwt"));
            var options = configuration.Get<JwtOptions>();
            configuration.GetSection("jwt").Bind(options);
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                cfg =>
                     {
                         cfg.RequireHttpsMetadata = false;
                         cfg.SaveToken = true;
                         cfg.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateAudience = false,
                             ValidIssuer = options.Issuer,//configuration["jwt:issuer"],
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecurityKey/*configuration["jwt:securityKey"]*/)),
                             ValidateLifetime = true
                         };
                     });
        }
    }
}