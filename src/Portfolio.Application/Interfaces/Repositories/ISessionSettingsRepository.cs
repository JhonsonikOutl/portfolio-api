using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces.Repositories
{
    public interface ISessionSettingsRepository
    {
        Task<SessionSettings?> GetAsync();

        Task<SessionSettings> UpdateAsync(SessionSettings settings);
    }
}
