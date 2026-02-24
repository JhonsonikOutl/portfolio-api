namespace Portfolio.Domain.ValueObject
{
    /// <summary>
    /// Resultado interno del seeding (usado por el repository)
    /// </summary>
    public class SeedDataResult
    {
        public int TotalCreated { get; set; }
        public int ProfileCreated { get; set; }
        public int SkillsCreated { get; set; }
        public int ExperiencesCreated { get; set; }
        public int EducationCreated { get; set; }
        public int ProjectsCreated { get; set; }
        public int SessionSettingsCreated { get; set; }
    }
}
