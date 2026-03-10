using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Portfolio.Application.Interfaces.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Portfolio.Infrastructure.Services
{
    /// <summary>
    /// Implementación de EmailService usando SendGrid.
    /// </summary>
    public class SendGridEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SendGridEmailService> _logger;

        public SendGridEmailService(
            IConfiguration configuration,
            ILogger<SendGridEmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            return await SendEmailAsync(to, subject, body, false);
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml)
        {
            try
            {
                var apiKey = _configuration["EmailSettings:SendGridApiKey"];
                var fromEmail = _configuration["EmailSettings:FromEmail"];
                var fromName = _configuration["EmailSettings:FromName"];

                if (string.IsNullOrEmpty(apiKey))
                {
                    _logger.LogError("SendGrid API Key no configurada");
                    return false;
                }

                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(fromEmail, fromName);
                var toEmail = new EmailAddress(to);

                var msg = MailHelper.CreateSingleEmail(
                    from,
                    toEmail,
                    subject,
                    isHtml ? null : body,
                    isHtml ? body : null
                );

                var response = await client.SendEmailAsync(msg);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Email enviado exitosamente a: {To}", to);
                    return true;
                }
                else
                {
                    _logger.LogError("Error al enviar email. StatusCode: {StatusCode}", response.StatusCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar email con SendGrid");
                return false;
            }
        }
    }
}
