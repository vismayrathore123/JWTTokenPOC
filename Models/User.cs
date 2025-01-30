using System.ComponentModel.DataAnnotations;

namespace JWTTokenAuthenticationPOC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string Role { get; set; } = "User";
        [Required]
        public string PasswordHash { get; set; }
    }
}
