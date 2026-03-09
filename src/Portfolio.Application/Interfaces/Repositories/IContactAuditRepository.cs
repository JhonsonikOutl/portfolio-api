using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces.Repositories
{
    public interface IContactAuditRepository : IRepository<ContactAudit>
    {
        Task<int> GetDailyCountAsync(DateTime date);
    }
}
