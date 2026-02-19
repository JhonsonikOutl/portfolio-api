namespace Portfolio.Domain.Entities
{
    public class Education
    {
        public string Id { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string FieldOfStudy { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentlyStudying { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}
