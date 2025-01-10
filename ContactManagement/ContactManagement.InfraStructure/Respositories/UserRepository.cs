using ContactManagement.Domain.Interfaces;
using ContactManagement.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ContactManagement.InfraStructure.Respositories
{
	public class UserRepository: IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var query = "SELECT * FROM Users WHERE Username = @Username";

            using (var connection = CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
            }
        }

        public async Task AddAsync(User user)
        {
            var query = "INSERT INTO Users (Username, Password, SystemPermission) VALUES (@Username, @Password, @SystemPermission)";

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(query, user);
            }
        }

       
    }
}
