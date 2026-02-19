namespace Portfolio.Application.DTOs.Experience
{
    public class CreateExperienceDto
    {
        public string Company { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Achievements { get; set; } = new();
        public List<string> Technologies { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentJob { get; set; }
        public int DisplayOrder { get; set; }
    }
}