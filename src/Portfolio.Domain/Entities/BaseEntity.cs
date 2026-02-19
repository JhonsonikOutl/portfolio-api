namespace Portfolio.Domain.Entities
{
    /// <summary>
    /// Clase base para todas las entidades del dominio.
    /// Implementa propiedades comunes (Id, fechas de auditoría).
    /// </summary>
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}