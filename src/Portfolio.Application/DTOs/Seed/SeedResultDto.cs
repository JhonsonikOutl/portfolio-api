namespace Portfolio.Application.DTOs.Seed
{
    /// <summary>
    /// DTO con el resultado de la operación de seed.
    /// </summary>
    public class SeedResultDto
    {
        public int ProfileCreated { get; set; }
        public int SkillsCreated { get; set; }
        public int ExperiencesCreated { get; set; }
        public int ProjectsCreated { get; set; }

        public int TotalCreated => SkillsCreated + ExperiencesCreated + ProjectsCreated;
    }
}
