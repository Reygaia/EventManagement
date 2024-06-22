using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsSetup.Authentication
{
    public class JwtBearerOptionsSetup : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly JwtOptions _options;

        public JwtBearerOptionsSetup(IOptions<JwtOptions> options) 
        {
            _options = options.Value;
        }

        public void PostConfigure(string? name, JwtBearerOptions options)
        {
            options.TokenValidationParameters.ValidIssuer = _options.Issuer;
            options.TokenValidationParameters.ValidAudience = _options.Audience;
            options.TokenValidationParameters.IssuerSigningKey = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            options.TokenValidationParameters.ValidateIssuerSigningKey = true;
            options.TokenValidationParameters.ValidateIssuer = true;
            options.TokenValidationParameters.ValidateAudience = true;
            options.TokenValidationParameters.ValidateLifetime = true;
        }
    }
}
