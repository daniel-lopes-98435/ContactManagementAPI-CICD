using ContactManagement.Domain.Entities;

namespace ContactManagement.Application.Interfaces;

/// <summary>
/// Interface for the service that handles business logic related to contacts.
/// </summary>
public interface IContactServices
{
    /// <summary>
    /// Retrieves a contact by its unique identifier.
    /// </summary>
    /// <param name="id">The identifier of the contact.</param>
    /// <returns>The contact with the specified identifier.</returns>
    Task<Contact> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all contacts.
    /// </summary>
    /// <returns>A list of all contacts.</returns>
    Task<IEnumerable<Contact>> GetAllAsync();

    /// <summary>
    /// Returns all contacts from the same area code entered
    /// </summary>
    /// <param name="areaCode"></param>
    /// <returns></returns>
    Task<IEnumerable<Contact>> GetByAreaCodeAsync(int areaCode);


	/// <summary>
	/// Adds a new contact to the repository.
	/// </summary>
	/// <param name="contact">The contact to add.</param>
	/// <returns>The identifier of the newly added contact.</returns>
	Task<int> AddAsync(Contact contact);

    /// <summary>
    /// Updates an existing contact in the repository.
    /// </summary>
    /// <param name="contact">The contact with updated information.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(Contact contact);

    /// <summary>
    /// Deletes a contact by its unique identifier.
    /// </summary>
    /// <param name="id">The identifier of the contact to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteAsync(int id);
}