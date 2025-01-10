using ContactManagement.Domain.Entities;

namespace ContactManagement.Application.Interfaces
{
	public interface ITokenService
    {
        Task<string> GetToken(User user); // Método deve ser assíncrono
    }
}
