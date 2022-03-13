using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFleetAPI.Entities
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PkUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? AccessToken { get; set; }
        public string? Roles { get; set; }
        public bool? IsAuthenticated { get; set; }
    }
}
