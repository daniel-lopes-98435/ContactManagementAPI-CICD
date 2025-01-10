using Swashbuckle.AspNetCore.Annotations;

namespace ContactManagement.Application.Dtos;

public class ContactDto
{ 

    [SwaggerSchema("The first name of the contact.")]
    public string FirstName { get; set; }
    
    [SwaggerSchema("The last name of the contact.")]
    public string LastName { get; set; }
    
    [SwaggerSchema("The area code for the contact's phone number.")]    
    public string AreaCode { get; set; }
    
    [SwaggerSchema("The contact's phone number.")]
    public string PhoneNumber { get; set; }
    
    [SwaggerSchema("The contact's email address.")]
    public string Email { get; set; }
    
}