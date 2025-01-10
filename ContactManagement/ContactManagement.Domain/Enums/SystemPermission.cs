using Swashbuckle.AspNetCore.Annotations;

namespace ContactManagement.Domain.Enums
{
	[SwaggerSchema("System permission is the user level, [0] Admin, [1] User, [2] Guest")]
    /// <summary>
    /// Enum representing different system permission levels for a user.
    /// </summary>
    public enum SystemPermissionType
    {
        /// <summary>
        /// Admin level permission.
        /// </summary>
        Admin,
        
        /// <summary>
        /// User level permission.
        /// </summary>
        User,
        
        /// <summary>
        /// Guest level permission.
        /// </summary>
        Guest
    }
    
    /// <summary>
    /// Class used for define System Permisions
    /// </summary>
    public static class SystemPermission
    {
        public const string Admin = "Admin";
        public const string Usuario = "User";
        public const string Convidado = "Guest";
    }
}
