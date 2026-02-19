namespace Portfolio.Domain.Entities
{
    public class Profile : BaseEntity
    {
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string PhotoUrl { get; set; }
        public string Bio { get; set; }
        public bool AvailableToJob { get; set; }
        public ProfileSocialLinks SocialLinks { get; set; } = new();
    }
}