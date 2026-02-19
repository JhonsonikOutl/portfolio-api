using Portfolio.Application.DTOs.Profile;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IExperienceRepository _experienceRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICvGeneratorService _cvGeneratorService;

        public ProfileService(
            IProfileRepository profileRepository,
            IExperienceRepository experienceRepository,
            ISkillRepository skillRepository,
            IEducationRepository educationRepository,
            IProjectRepository projectRepository,
            ICvGeneratorService cvGeneratorService)
        {
            _profileRepository = profileRepository;
            _experienceRepository = experienceRepository;
            _skillRepository = skillRepository;
            _educationRepository = educationRepository;
            _projectRepository = projectRepository;
            _cvGeneratorService = cvGeneratorService;
        }

        public async Task<ProfileDto?> GetProfileAsync()
        {
            var profile = await _profileRepository.GetProfileAsync();
            return profile != null ? MapToDto(profile) : null;
        }

        public async Task<ProfileDto> UpdateProfileAsync(UpdateProfileDto updateDto)
        {
            var profile = new Profile
            {
                FullName = updateDto.FullName,
                Title = updateDto.Title,
                Email = updateDto.Email,
                Phone = updateDto.Phone,
                Location = updateDto.Location,
                PhotoUrl = updateDto.PhotoUrl,
                Bio = updateDto.Bio,
                AvailableToJob = updateDto.AvailableToJob,
                SocialLinks = new ProfileSocialLinks
                {
                    LinkedIn = updateDto.SocialLinks.LinkedIn,
                    GitHub = updateDto.SocialLinks.GitHub,
                    Twitter = updateDto.SocialLinks.Twitter,
                    Website = updateDto.SocialLinks.Website,
                    Email = updateDto?.SocialLinks.Email,
                    Whatsapp = updateDto?.SocialLinks.Whatsapp,
                }
            };

            var updated = await _profileRepository.CreateOrUpdateProfileAsync(profile);
            return MapToDto(updated);
        }

        public async Task<byte[]> GenerateCvAsync()
        {
            var profile = await _profileRepository.GetProfileAsync();
            if (profile == null)
                throw new Exception("Profile not found");

            var experiences = await _experienceRepository.GetAllAsync();
            var skills = await _skillRepository.GetAllAsync();
            var projects = await _projectRepository.GetAllAsync();
            var educations = await _educationRepository.GetAllAsync();

            return await _cvGeneratorService.GenerateCv(profile, experiences, skills, projects, educations);
        }

        private static ProfileDto MapToDto(Profile profile)
        {
            return new ProfileDto
            {
                Id = profile.Id,
                FullName = profile.FullName,
                Title = profile.Title,
                Email = profile.Email,
                Phone = profile.Phone,
                Location = profile.Location,
                PhotoUrl = profile.PhotoUrl,
                Bio = profile.Bio,
                AvailableToJob = profile.AvailableToJob,
                SocialLinks = new ProfileSocialLinksDto
                {
                    LinkedIn = profile.SocialLinks.LinkedIn,
                    GitHub = profile.SocialLinks.GitHub,
                    Twitter = profile.SocialLinks.Twitter,
                    Website = profile.SocialLinks.Website,
                    Email = profile.SocialLinks.Email,
                    Whatsapp = profile.SocialLinks.Whatsapp,
                }
            };
        }
    }
}