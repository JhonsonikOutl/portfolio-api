namespace Portfolio.Application.DTOs.Project
{
    /// <summary>
    /// DTO para lectura de proyectos (GET).
    /// Incluye el Id porque ya existe en la base de datos.
    /// </summary>
    public class ProjectDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Technologies { get; set; } = new();
        public string? ImageUrl { get; set; }
        public string? GithubUrl { get; set; }
        public string? LiveUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsFeatured { get; set; }
        public int DisplayOrder { get; set; }
    }
}
