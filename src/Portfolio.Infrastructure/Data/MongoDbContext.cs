using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Data
{
    /// <summary>
    /// Contexto de MongoDB.
    /// Maneja la conexión y expone las colecciones (tablas).
    /// </summary>
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            ConfigureGuidSerialization();

            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        private void ConfigureGuidSerialization()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(BaseEntity)))
            {
                BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            }
        }

        public IMongoCollection<Project> Projects =>
            _database.GetCollection<Project>("projects");

        public IMongoCollection<Skill> Skills =>
            _database.GetCollection<Skill>("skills");

        public IMongoCollection<Experience> Experiences =>
            _database.GetCollection<Experience>("experiences");

        public IMongoCollection<ContactMessage> ContactMessages =>
            _database.GetCollection<ContactMessage>("contactMessages");

        public IMongoCollection<User> Users =>
            _database.GetCollection<User>("users");

        public IMongoCollection<Profile> Profiles =>
            _database.GetCollection<Profile>("profile");

        public IMongoCollection<Education> Educations =>
            _database.GetCollection<Education>("education");
    }
}
