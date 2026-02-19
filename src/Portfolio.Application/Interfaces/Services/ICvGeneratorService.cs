using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces.Services
{
    public interface ICvGeneratorService
    {
        Task<byte[]> GenerateCv(
            Profile profile,
            IEnumerable<Experience> experiences,
            IEnumerable<Skill> skills,
            IEnumerable<Project> projects,
            IEnumerable<Education> educations);
    }
}
