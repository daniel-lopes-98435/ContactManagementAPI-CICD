using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ContactManagement.Api.Configurations;
public static class IdentityConfig
{
    public static IServiceCollection AddIndentity(this IServiceCollection services, IConfiguration configuration)
    {
        //Para rodar localmente
        //var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretJWT"));
        
        #region Teste para App service
        // Se estiver no ambiente de produção, obtém a variável de ambiente
        var secretJWT = configuration.GetValue<string>("SecretJWT");

        if (string.IsNullOrEmpty(secretJWT))
        {
            // Se o segredo estiver ausente, pode lançar um erro ou lidar de outra forma
            throw new ArgumentNullException("SecretJWT", "O segredo JWT não pode ser nulo ou vazio.");
        }
        #endregion

        var key = Encoding.ASCII.GetBytes(secretJWT);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;
    }
}

