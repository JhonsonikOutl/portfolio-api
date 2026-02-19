namespace Portfolio.Infrastructure.Data
{
    /// <summary>
    /// Modelo para configuración de MongoDB desde appsettings.json.
    /// Se enlaza automáticamente con la sección "MongoDbSettings".
    /// </summary>
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public int ConnectionTimeoutSeconds { get; set; } = 30;
    }
}
