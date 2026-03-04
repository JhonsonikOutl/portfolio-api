using MongoDB.Driver;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio específico para educación.
    /// Hereda de Repository<Education> (tiene CRUD básico).
    /// Implementa IEducationRepository (métodos específicos).
    /// </summary>
    public class EducationRepository : Repository<Education>, IEducationRepository
    {
        public EducationRepository(MongoDbContext context)
            : base(context.Educations)
        {
        }

        public async Task<IEnumerable<Education>> GetAllOrderedAsync()
        {
            var sortDefinition = Builders<Education>.Sort.Ascending(e => e.DisplayOrder);

            return await _collection
                .Find(_ => true)
                .Sort(sortDefinition)
                .ToListAsync();
        }

        public async Task<IEnumerable<Education>> GetCurrentEducationsAsync()
        {
            var filter = Builders<Education>.Filter.Eq(e => e.IsCurrentlyStudying, true);
            var sortDefinition = Builders<Education>.Sort.Ascending(e => e.DisplayOrder);

            return await _collection
                .Find(filter)
                .Sort(sortDefinition)
                .ToListAsync();
        }
    }
}