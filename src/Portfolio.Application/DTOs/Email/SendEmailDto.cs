namespace Portfolio.Application.DTOs.Email
{
    /// <summary>
    /// DTO genérico para enviar emails.
    /// </summary>
    public class SendEmailDto
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool IsHtml { get; set; } = false;
    }
}
