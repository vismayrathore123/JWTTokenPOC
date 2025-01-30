using System.ComponentModel.DataAnnotations;

namespace JWTTokenAuthenticationPOC.Models
{
    public class Token
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string JwtToken { get; set; }
        public DateTime ExprityDate { get; set; }
    }
}
