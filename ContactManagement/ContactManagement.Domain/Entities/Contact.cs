using System.Diagnostics;

namespace ContactManagement.Domain.Entities;
/// <summary>
/// Represents a Contact entity in the domain.
/// </summary>
public class Contact
{
    /// <summary>
    /// Gets the unique identifier of the Contact.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets the first name of the Contact.
    /// </summary>
    public string FirstName { get; private set; }
    
    /// <summary>
    /// Gets the last name of the Contact.
    /// </summary>
    public string LastName { get; private set; }
    
    /// <summary>
    /// Gests the area code of the Contact
    /// </summary>
    public string AreaCode { get; private set; }
    
    /// <summary>
    /// Gets the phone number of the Contact.
    /// </summary>
    public string PhoneNumber { get; private set; }
    
    /// <summary>
    /// Gets the email of the Contact.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Only use for ORM
    /// </summary>
    protected Contact()
    {
            
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Contact"/> class.
    /// </summary>
    /// <param name="firstName">The new first name of the contact.</param>
    /// <param name="lastName">The new last name of the contact.</param>
    /// <param name="email">The new email of the contact.</param>
    /// /// <param name="areaCode">The new are code of the contact.</param>
    /// <param name="phoneNumber">The new phone number of the contact.</param>
    public Contact(int Id, string firstName, string lastName, string areaCode, string phoneNumber, string email)
    {
        this.Id = Id;
        FirstName = firstName;
        LastName = lastName;
        AreaCode = areaCode;
        PhoneNumber = phoneNumber;
        Email = email;
    }
 
    
    /// <summary>
    /// Updates the contact's information.
    /// </summary>
    /// <param name="firstName">The new first name of the contact.</param>
    /// <param name="lastName">The new last name of the contact.</param>
    /// <param name="email">The new email of the contact.</param>
    /// /// <param name="areaCode">The new are code of the contact.</param>
    /// <param name="phoneNumber">The new phone number of the contact.</param>
    public void UpdateContact(string firstName, string lastName, string areaCode, string phoneNumber, string email)
     {
         FirstName = firstName;
         LastName = lastName;
         AreaCode = areaCode;
         PhoneNumber = phoneNumber;
         Email = email;
    }
}