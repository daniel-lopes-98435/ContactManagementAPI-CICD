using ContactManagement.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace ContactManagement.Domain.Entities;

public static class ListaUsuario
{
	public static IList<User> users { get; set; }
}

/// <summary>
/// Represents a User in the system.
/// </summary>
public class User
{
	[SwaggerSchema("Unique identifier for the user")]
	/// <summary>
	/// Unique identifier for the user.
	/// </summary>
	public int Id { get; set; }

	[SwaggerSchema("Username for the user")]
	/// <summary>
	/// The username of the user.
	/// </summary>
	public string Username { get; set; }

	[SwaggerSchema("Password for the user")]
	/// <summary>
	/// The password for the user.
	/// </summary>
	public string Password { get; set; }

	[SwaggerSchema("The system permission level for the user")]
	/// <summary>
	/// The system permission level for the user.
	/// </summary>
	public SystemPermissionType SystemPermission { get; set; }

	/// <summary>
	/// Only use for ORM
	/// </summary>
	protected User()
    {
        
    }
	/// <summary>
	/// Initializes a new instance of the <see cref="User"/> class.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="username"></param>
	/// <param name="password"></param>
	public User(int id, string username, string password)
    {
        this.Id = id;
		this.Username = username;
		this.Password = password;
    }
}