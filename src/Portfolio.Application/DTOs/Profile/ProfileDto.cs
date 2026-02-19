namespace Portfolio.Application.DTOs.Profile
{
    public class ProfileDto
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        public string Bio { get; set; } = string.Empty;
        public bool AvailableToJob { get; set; }
        public ProfileSocialLinksDto SocialLinks { get; set; } = new();
    }
}
