using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces.Repositories
{
    public interface IProfileRepository
    {
        Task<Profile?> GetProfileAsync();
        Task<Profile> CreateOrUpdateProfileAsync(Profile profile);
    }
}