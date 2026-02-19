namespace Portfolio.Domain.Entities
{
    /// <summary>
    /// Representa un mensaje enviado desde el formulario de contacto.
    /// </summary>
    public class ContactMessage : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }
}
