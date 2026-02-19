namespace Portfolio.Domain.Entities
{
    /// <summary>
    /// Representa una habilidad técnica o skill del portfolio.
    /// </summary>
    public class Skill : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Level { get; set; }
        public string? Icon { get; set; }
        public int DisplayOrder { get; set; }
    }
}