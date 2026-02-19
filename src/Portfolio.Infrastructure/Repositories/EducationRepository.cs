using MongoDB.Driver;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly IMongoCollection<Education> _collection;

        public EducationRepository(MongoDbContext context)
        {
            _collection = context.Educations;
        }

        public async Task<IEnumerable<Education>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Education?> GetByIdAsync(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Education> CreateAsync(Education education)
        {
            await _collection.InsertOneAsync(education);
            return education;
        }

        public async Task<Education?> UpdateAsync(string id, Education education)
        {
            education.Id = id;
            var result = await _collection.ReplaceOneAsync(e => e.Id == id, education);
            return result.ModifiedCount > 0 ? education : null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(e => e.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
