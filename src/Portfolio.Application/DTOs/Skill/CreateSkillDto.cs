namespace Portfolio.Application.DTOs.Skill
{
    public class CreateSkillDto
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Level { get; set; }
        public string? Icon { get; set; }
        public int DisplayOrder { get; set; }
    }
}
