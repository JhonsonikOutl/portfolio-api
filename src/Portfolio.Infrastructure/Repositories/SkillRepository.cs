using MongoDB.Driver;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        public SkillRepository(MongoDbContext context)
            : base(context.Skills)
        {
        }

        public async Task<IEnumerable<Skill>> GetByCategoryAsync(string category)
        {
            return await _collection
                .Find(s => s.Category == category)
                .SortBy(s => s.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            var filter = Builders<Skill>.Filter.Empty;
            return await _collection
                .Distinct<string>("Category", filter)
                .ToListAsync();
        }
    }
}
