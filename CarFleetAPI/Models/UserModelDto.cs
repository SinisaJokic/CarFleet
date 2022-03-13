using System.Text.Json.Serialization;

namespace CarFleetAPI.Models
{
    public class UserModelDto
    {
        public int PkUser { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? AccessToken { get; set; }
        public string? Roles { get; set; }
        public bool? IsAuthenticated { get; set; }
    }
}
