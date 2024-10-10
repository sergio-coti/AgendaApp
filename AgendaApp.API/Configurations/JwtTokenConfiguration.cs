using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AgendaApp.API.Configurations
{
    /// <summary>
    /// Classe de configuração para definir a politica de
    /// autenticação do projeto (JWT - JSON WEB TOKENS)
    /// </summary>
    public class JwtTokenConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new  SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes("E97A6687-96AC-408A-9E97-D2CF8202EA4D"))
                };
            });
        }
    }
}
