
namespace PLECSYS_Studio.Wrappers.Users
{
    public class LoginResponse
    {
        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? First_lastname { get; set; }

        public string? Second_lastname { get; set; }

        public string? Phone { get; set; }

        public DateTime? Created_at { get; set; }

        public bool Is_logged { get; set; }
    }
}
