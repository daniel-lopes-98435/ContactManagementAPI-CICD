using System.Data;
using ContactManagement.Domain.Entities;
using ContactManagement.Domain.Interfaces;
using Dapper;

namespace ContactManagement.InfraStructure.Respositories;

    /// <summary>
    /// Implements the <see cref="IContactRepository"/> interface using Dapper for data access.
    /// </summary>
    public class ContactRepository : IContactRepository
    {
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        public ContactRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <inheritdoc/>
        public async Task<Contact> GetByIdAsync(int id)
        {
            const string query = "SELECT * FROM Contacts WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Contact>(query, new { Id = id });
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            const string query = "SELECT * FROM Contacts";
            return await _dbConnection.QueryAsync<Contact>(query);
        }

	    /// <inheritdoc/>
	    public async Task<IEnumerable<Contact>> GetByAreaCodeAsync(int areaCode)
	    {
		    const string query = "SELECT * FROM Contacts WHERE AreaCode = @AreaCode";
		    return await _dbConnection.QueryAsync<Contact>(query, new { AreaCode = areaCode });
	    }

	/// <inheritdoc/>
	    public async Task<int> AddAsync(Contact contact)
        {
            const string query = @"
                INSERT INTO Contacts (FirstName, LastName, AreaCode, PhoneNumber, Email) 
                VALUES (@FirstName, @LastName, @AreaCode, @PhoneNumber, @Email);
                SELECT CAST(SCOPE_IDENTITY() as int)";
            return await _dbConnection.ExecuteScalarAsync<int>(query, contact);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Contact contact)
        {
            const string query = @"
                UPDATE Contacts 
                SET FirstName = @FirstName, LastName = @LastName, AreaCode = @AreaCode, PhoneNumber = @PhoneNumber, Email = @Email 
                WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, contact);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            const string query = "DELETE FROM Contacts WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }