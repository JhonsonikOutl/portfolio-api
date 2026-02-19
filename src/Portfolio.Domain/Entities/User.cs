namespace Portfolio.Domain.Entities
{
    /// <summary>
    /// Representa un usuario administrador del sistema.
    /// Para MVP, solo habrá un usuario admin.
    /// </summary>
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Admin";
    }
}
