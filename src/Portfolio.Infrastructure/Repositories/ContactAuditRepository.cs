using MongoDB.Driver;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio de auditoría de contacto.
    /// Consulta el conteo diario para generar el secuencial del radicado.
    /// </summary>
    public class ContactAuditRepository : Repository<ContactAudit>, IContactAuditRepository
    {
        public ContactAuditRepository(MongoDbContext context)
            : base(context.ContactAudits)
        {
        }

        public async Task<int> GetDailyCountAsync(DateTime date)
        {
            var startOfDay = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);
            var endOfDay = startOfDay.AddDays(1);

            var filter = Builders<ContactAudit>.Filter.And(
                Builders<ContactAudit>.Filter.Gte(a => a.CreatedAt, startOfDay),
                Builders<ContactAudit>.Filter.Lt(a => a.CreatedAt, endOfDay)
            );

            var count = await _collection.CountDocumentsAsync(filter);

            return (int)count;
        }
    }
}
