using System.ComponentModel.DataAnnotations;

namespace jwt.Models
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleName { get; set; }

    }
}
