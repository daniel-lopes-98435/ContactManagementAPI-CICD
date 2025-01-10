using ContactManagement.Application.Interfaces;
using ContactManagement.Domain.Entities;
using ContactManagement.Domain.Interfaces;

namespace ContactManagement.Application.Services;

/// <summary>
/// Service that handles business logic related to contacts.
/// </summary>
public class ContactServices : IContactServices
{
    private readonly IContactRepository _repository;

    public ContactServices(IContactRepository repository)
    {
        _repository = repository;
    }
    public async Task<Contact> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

	public async Task<IEnumerable<Contact>> GetByAreaCodeAsync(int areaCode)
	{
		return await _repository.GetByAreaCodeAsync(areaCode);
	}

	public async Task<int> AddAsync(Contact contact)
    {
        return await _repository.AddAsync(contact);
    }

    public async Task UpdateAsync(Contact contact)
    {
        await _repository.UpdateAsync(contact);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}