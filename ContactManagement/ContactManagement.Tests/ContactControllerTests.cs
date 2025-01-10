using ContactManagement.Api.Controllers;
using ContactManagement.Application.Dtos;
using ContactManagement.Application.Interfaces;
using ContactManagement.Domain.Entities;
using ContactManagement.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ContactManagement.Tests;

public class ContactControllerTests
{
    private readonly Mock<IContactServices> _mockServices;
    private readonly ContactController _contactController;
    private readonly Mock<ILogger<ContactController>> _mockLogger;
    public ContactControllerTests()
    {
        _mockServices = new Mock<IContactServices>();
        _mockLogger = new Mock<ILogger<ContactController>>();
        _contactController = new ContactController(_mockServices.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAllContacts_ReturnsOk_WhenContactsExist()
    {
        
        var contactListDto = new List<ContactDto>()
        {new ContactDto
            {
                FirstName = "John",
                LastName = "Doe",
                AreaCode = "33",
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com"
            },
            
            new ContactDto
            {
                FirstName = "Mary",
                LastName = "Doe",
                AreaCode = "33",
                PhoneNumber = "1234567899",
                Email = "may.doe@example.com"
            }
        };
        var contactList = new List<Contact>()
        {
            new Contact(1, "John", "Doe", "33", "1234567890", "john.doe@example.com"),
            new Contact(1, "Mary", "Doe", "33", "1234567899", "mary.doe@example.com")
        };
        _mockServices.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(contactList);
        
        var result = await _contactController.GetAllContacts();
        
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(2, contactList.Count);
    }

    [Fact]
    public async Task GetContactById_ReturnsOk_WhenContactExists()
    {
        
        var contactDto = new ContactDto
        {
            FirstName = "John",
            LastName = "Doe",
            AreaCode = "33",
            PhoneNumber = "1234567890",
            Email = "john.doe@example.com"
        };
        
        var contact = new Contact(1, "John", "Doe", "33", "1234567890", "john.doe@example.com");
       
        _mockServices.Setup(repo => repo.GetByIdAsync(contact.Id))
            .ReturnsAsync(contact);
        
        var result = await _contactController.GetContactById(contact.Id);
        
      
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        
        Assert.IsType<ContactDto>(okResult.Value);
    }

    [Fact]
    public async Task GetContactById_ReturnsNotFound_WhenContactDoesNotExist()
    {
        var contactId = 2;
        var contact = new Contact(1, "John", "Doe", "33", "1234567890", "john.doe@example.com");
        
        _mockServices.Setup(repo => repo.GetByIdAsync(contact.Id))
            .ReturnsAsync(contact);
        
        var result = await _contactController.GetContactById(contactId);
        
        Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.NotEqual(contact.Id, contactId);
        
    }

    [Fact]
    public async Task AddContact_ReturnsCreated_WhenContactsExist()
    {
        var contactDto = new ContactDto
        {
            FirstName = "John",
            LastName = "Doe",
            AreaCode = "33",
            PhoneNumber = "1234567890",
            Email = "john.doe@example.com"
        };

        var contact = new Contact(1, "John", "Doe", "33", "1234567890", "john.doe@example.com");

        _mockServices.Setup(repo => repo.AddAsync(contact))
            .ReturnsAsync(1);
        
        var result = await _contactController.AddContact(contactDto);
        Assert.IsType<CreatedAtActionResult>(result.Result);
    }

    [Fact]
    public async Task UpdateContact_ReturnsOk_WhenContactsExist()
    {
        var contactDto = new ContactDto
        {
            FirstName = "John",
            LastName = "Doe",
            AreaCode = "33",
            PhoneNumber = "1234567890",
            Email = "john.doe@example.com"
        };
        
        var contact = new Contact(1, "John", "Doe", "33", "1234567890", "john.doe@example.com");
        _mockServices.Setup(repo => repo.UpdateAsync(contact))
            .Returns(Task.CompletedTask);
        
        var result = await _contactController.UpdateContact(contact.Id, contactDto);
        Assert.IsType<NotFoundResult>(result);
            
    }
    
    [Fact]
    public async Task UpdateContact_ReturnsBadRequest_WhenIdDoesNotMatch()
    {
        var contactDto = new ContactDto
        {
            FirstName = "John",
            LastName = "Doe",
            AreaCode = "33",
            PhoneNumber = "1234567890",
            Email = "john.doe@example.com"
        };
        var invalidId = 2;
        var contact = new Contact(1, "John", "Doe", "33", "1234567890", "john.doe@example.com");

        
        _mockServices.Setup(repo => repo.UpdateAsync(contact))
            .Returns(Task.CompletedTask);
        
        var result = await _contactController.UpdateContact(invalidId, contactDto);
        Assert.IsType<NotFoundResult>(result);
            
    }   
    
    [Fact]
    public async Task UpdateContact_ReturnsNoContent_WhenContactIsUpdated()
    {
        // Arrange
        var contactId = 1;
        var contactDto = new ContactDto { FirstName = "John", LastName = "Doe" };
        var existingContact = new Contact(contactId, "John", "Doe", "33", "1234567890", "john.doe@example.com");

        _mockServices.Setup(repo => repo.GetByIdAsync(contactId))
            .ReturnsAsync(existingContact);
        _mockServices.Setup(repo => repo.UpdateAsync(existingContact))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _contactController.UpdateContact(contactId, contactDto);

        // Assert
        Assert.IsType<NoContentResult>(result);

        // // Verify that LogInformation was called with the correct message and contactId
        // _mockLogger.Verify(
        //     x => x.LogInformation(It.Is<string>(s => s == "The contact was successfully updated with id: {Id}"),
        //         It.IsAny<int>()),
        //     Times.Once);

    }

    
    
    [Fact]
    public async Task DeleteContact_ReturnsNoContent_WhenContactIsDeleted()
    {
        var contactId = 1;
        var contact = new Contact(contactId, "John", "Doe", "33", "1234567890", "john.doe@example.com");
        
        _mockServices.Setup(repo => repo.GetByIdAsync(contactId))
            .ReturnsAsync(contact);

        _mockServices.Setup(repo => repo.DeleteAsync(contactId))
            .Returns(Task.CompletedTask);
        
        var result = await _contactController.DeleteContact(contactId);
        
        Assert.IsType<NoContentResult>(result); 
        
        _mockServices.Verify(repo => repo.DeleteAsync(contactId), Times.Once);

    }
    
    [Fact]
    public async Task DeleteContact_ReturnsNotFound_WhenContactDoesNotExist()
    {
        var contactId = 2;

        _mockServices.Setup(repo => repo.GetByIdAsync(contactId))
            .ReturnsAsync((Contact)null); 
        
        var result = await _contactController.DeleteContact(contactId);
        
        Assert.IsType<NotFoundObjectResult>(result);
        
        _mockServices.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        
    }

}
