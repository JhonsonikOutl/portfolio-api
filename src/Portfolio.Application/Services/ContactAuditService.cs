using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services
{
    /// <summary>
    /// Genera el número de radicado y persiste la auditoría.
    /// </summary>
    public class ContactAuditService : IContactAuditService
    {
        private readonly IContactAuditRepository _auditRepository;

        public ContactAuditService(IContactAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public async Task<string> GenerateRadicateAsync(Guid messageId)
        {
            var today = DateTime.UtcNow;
            var dailyCount = await _auditRepository.GetDailyCountAsync(today);
            var sequential = (dailyCount + 1).ToString("D4");
            var radicateNumber = $"RAD-{today:yyyyMMdd}-{sequential}";

            var audit = new ContactAudit
            {
                Id = Guid.NewGuid(),
                MessageId = messageId,
                RadicateNumber = radicateNumber
            };

            await _auditRepository.CreateAsync(audit);

            return radicateNumber;
        }
    }
}
