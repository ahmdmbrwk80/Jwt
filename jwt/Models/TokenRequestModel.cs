using System.ComponentModel.DataAnnotations;

namespace jwt.Models
{
    public class TokenRequestModel
    {

        [Required]
        public string Email { get; set; }
        [Required]
        public string PassWord { get; set; }
    }
}
