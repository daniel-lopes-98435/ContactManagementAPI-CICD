using ContactManagement.Application.Interfaces;
using ContactManagement.Domain.Interfaces;
using ContactManagement.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactManagement.Application.Services
{
	public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public TokenService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<string> GetToken(User user)
        {
            // Validamos se o usuário existe no banco de dados
            var usuarioDb = await _userRepository.GetByUsernameAsync(user.Username);

            // Verifica se o usuário foi encontrado e se a senha está correta
            if (usuarioDb == null || usuarioDb.Password != user.Password)
                return string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var chaveCriptografia = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretJWT"));

            var tokenPropriedades = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuarioDb.Username),
                    new Claim(ClaimTypes.Role, usuarioDb.SystemPermission.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chaveCriptografia),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenPropriedades);
            return tokenHandler.WriteToken(token);
        }
    }
}
