namespace ApiClubMed.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? UserRole { get; set; }
    }
}
