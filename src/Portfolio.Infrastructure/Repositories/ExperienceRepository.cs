using MongoDB.Driver;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio específico para experiencias laborales.
    /// </summary>
    public class ExperienceRepository : Repository<Experience>, IExperienceRepository
    {
        public ExperienceRepository(MongoDbContext context)
            : base(context.Experiences)
        {
        }

        public async Task<IEnumerable<Experience>> GetOrderedByDateAsync()
        {
            return await _collection
                .Find(_ => true)
                .SortByDescending(e => e.StartDate)
                .ToListAsync();
        }

        public async Task<Experience?> GetCurrentJobAsync()
        {
            return await _collection
                .Find(e => e.IsCurrentJob == true)
                .FirstOrDefaultAsync();
        }
    }
}
