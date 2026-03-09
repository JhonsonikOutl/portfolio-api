namespace Portfolio.Application.Interfaces.Services
{
    /// <summary>
    /// Servicio de infraestructura para envío de emails vía SMTP.
    /// NO usa DTOs, solo parámetros primitivos.
    /// </summary>
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body);

        Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml);
    }
}
