using jwt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jwt.Data
{
    public class ApplicationDBcontext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options)
                                   : base(options) { }
    }
}
