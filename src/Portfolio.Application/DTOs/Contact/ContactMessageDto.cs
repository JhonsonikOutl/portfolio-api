namespace Portfolio.Application.DTOs.Contact
{
    /// <summary>
    /// DTO para lectura de mensajes de contacto (solo admin).
    /// </summary>
    public class ContactMessageDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}