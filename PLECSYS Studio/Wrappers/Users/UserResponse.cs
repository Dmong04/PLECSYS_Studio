
namespace PLECSYS_Studio.Wrappers.Users
{
    public class UserResponse
    {
        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? First_lastname { get; set; }

        public string? Second_lastname { get; set; }

        public string? Phone { get; set; }

        public string Full_name => $"{Name} {First_lastname} {Second_lastname}";
    }
}
