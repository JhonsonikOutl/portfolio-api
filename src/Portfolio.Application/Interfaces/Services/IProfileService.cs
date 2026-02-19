using Portfolio.Application.DTOs.Profile;

namespace Portfolio.Application.Interfaces.Services
{
    public interface IProfileService
    {
        Task<ProfileDto?> GetProfileAsync();
        Task<ProfileDto> UpdateProfileAsync(UpdateProfileDto updateDto);
        Task<byte[]> GenerateCvAsync();
    }
}
