using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace jwt.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

    }
}
