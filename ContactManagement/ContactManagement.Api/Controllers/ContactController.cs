 using ContactManagement.Application.Dtos;
using ContactManagement.Application.Interfaces;
using ContactManagement.Domain.Entities;
using ContactManagement.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.Api.Controllers;

/// <summary>
/// Handles HTTP requests related to contact operations.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactServices _contactServices;
    private readonly ILogger<ContactController> _logger;

    /// <summary>
    /// private variable for the <see cref="IContactServices"/> class.
    /// </summary>
    /// <param name="contactServices">The repository for handling contact data.</param>
    public ContactController(IContactServices contactServices, ILogger<ContactController> logger)
    {
        _contactServices = contactServices;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all contacts.
    /// </summary>
    /// <returns>A list of all contacts.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactReadDto>>> GetAllContacts()
    {
            
        var contacts = await _contactServices.GetAllAsync();

        if (contacts is null)
        {
	        _logger.LogError("No contacts found");

            var errorResponse = new ErrorResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                Errors = new List<string> { "No contacts found." }
            };
            return NotFound(errorResponse);
        }
            
        _logger.LogInformation($"The list of contact was successfully received");
        return Ok(contacts.Select(contact => new ContactReadDto
        {
	        Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            AreaCode = contact.AreaCode,
            PhoneNumber = contact.PhoneNumber,
            Email = contact.Email,
        }).ToList());
    }

    /// <summary>
    /// Retrieves a contact by its unique identifier.
    /// </summary>
    /// <param name="id">The identifier of the contact.</param>
    /// <returns>The contact with the specified identifier.</returns>
    [HttpGet("{id}")]
	public async Task<ActionResult<ContactDto>> GetContactById(int id)
    {
        var contact = await _contactServices.GetByIdAsync(id);
        if (contact == null)
        {
	        _logger.LogError($"Thre is no contact found for the informed id: {id}");
            return NotFound($"Thre is no contact found for the informed id: {id}");
        }
            
        var contactDto = new ContactDto
        {
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            AreaCode = contact.AreaCode,
            PhoneNumber = contact.PhoneNumber,
            Email = contact.Email,

        };
            
        _logger.LogError($"Contact was successfully with id: {id}");    
        return Ok(contactDto);
    }
	
	/// <summary>
	/// Retrieves a contact by its area code.
	/// </summary>
	/// <param name="areaCode"></param>
	/// <returns></returns>
	[HttpGet("/{areaCode}")]
	public async Task<ActionResult<IEnumerable<ContactDto>>> GetContactByAreaCode(int areaCode)
	{
		var contacts = await _contactServices.GetByAreaCodeAsync(areaCode);
		if (contacts == null)
		{
			_logger.LogError($"Thre is no contact found for the informed Area code: {areaCode}");
			return NotFound($"Thre is no contact found for the informed Area code: {areaCode}");
		}
		    
		_logger.LogError($"Contact was successfully with area code: {areaCode}");    
		return Ok(contacts.Select(contact => new ContactDto
		{
			FirstName = contact.FirstName,
			LastName = contact.LastName,
			AreaCode = contact.AreaCode,
			PhoneNumber = contact.PhoneNumber,
			Email = contact.Email,
		}).ToList());
	}

	/// <summary>
	/// Adds a new contact, requires a user Token.
	/// </summary>
	/// <param name="contactDto">The contact to add.</param>
	/// <returns>The identifier of the newly added contact.</returns>
	[HttpPost]
    [Authorize(Roles = SystemPermission.Usuario + "," + SystemPermission.Admin)]
	public async Task<ActionResult<int>> AddContact(ContactDto contactDto)
    {
        var contact = new Contact(0, contactDto.FirstName, contactDto.LastName, contactDto.AreaCode, contactDto.PhoneNumber, contactDto.Email);

        var id = await _contactServices.AddAsync(contact);
        _logger.LogInformation($"Contact was successfully created with id: {id}");
        return CreatedAtAction(nameof(GetContactById), new { id }, contactDto);
    }

    /// <summary>
    /// Updates an existing contact, requires a user Token.
    /// </summary>
    /// <param name="id">The identifier of the contact to update.</param>
    /// <param name="contactDto">The updated contact information.</param>
    /// <returns>A status indicating the result of the operation.</returns>
    [HttpPut("{id}")]
	[Authorize(Roles = SystemPermission.Usuario + "," + SystemPermission.Admin)]
    public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactDto contactDto)
    {
        var existingContact = await _contactServices.GetByIdAsync(id);
        if (existingContact == null)
        {
	        _logger.LogError($"Thre is no contact found for the informed id: {id}");
            return NotFound();
        }
            
        existingContact.UpdateContact(contactDto.FirstName, contactDto.LastName, contactDto.AreaCode, contactDto.PhoneNumber, contactDto.Email); 
        await _contactServices.UpdateAsync(existingContact);
            
        _logger.LogInformation($"The contact was successfully updated with id: {id}");
        return NoContent();
    }

    /// <summary>
    /// Deletes a contact by its unique identifier, requires a admin Token.
    /// </summary>
    /// <param name="id">The identifier of the contact to delete.</param>
    /// <returns>A status indicating the result of the operation.</returns>
    [HttpDelete("{id}")]
	[Authorize(Roles = SystemPermission.Admin)]
    public async Task<IActionResult> DeleteContact(int id)
    {
            
	    var existingContact = await _contactServices.GetByIdAsync(id);
	    if (existingContact == null)
	    {
		    _logger.LogError($"No contact found for id: {id}");
		    return NotFound($"No contact found for id: {id}");
	    }
    
	    await _contactServices.DeleteAsync(id);
	    _logger.LogInformation($"The contact was successfully deleted with id: {id}");
    
	    return NoContent();
    }
}