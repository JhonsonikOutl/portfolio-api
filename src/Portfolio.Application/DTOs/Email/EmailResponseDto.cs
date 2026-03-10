namespace Portfolio.Application.DTOs.Email
{
    /// <summary>
    /// DTO de respuesta para operaciones de email.
    /// </summary>
    public class EmailResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? SentTo { get; set; }
    }
}
