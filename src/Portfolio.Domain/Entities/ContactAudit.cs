namespace Portfolio.Domain.Entities
{
    /// <summary>
    /// Entidad de auditoría para mensajes de contacto.
    /// </summary>
    public class ContactAudit : BaseEntity
    {
        public Guid MessageId { get; set; }
        public string RadicateNumber { get; set; } = string.Empty;
    }
}
