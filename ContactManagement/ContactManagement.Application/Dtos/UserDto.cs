using Swashbuckle.AspNetCore.Annotations;

namespace ContactManagement.Application.Dtos
{
	public class UserDto
    {
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
	}
}
