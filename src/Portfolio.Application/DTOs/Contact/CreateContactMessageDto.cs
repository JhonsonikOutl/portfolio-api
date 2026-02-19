namespace Portfolio.Application.DTOs.Contact
{
    /// <summary>
    /// DTO para envío de mensajes desde el formulario público.
    /// NO incluye IsRead ni CreatedAt (se generan automáticamente).
    /// </summary>
    public class CreateContactMessageDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
