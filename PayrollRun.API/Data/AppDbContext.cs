using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data
{
    public class AppDbContext :IdentityDbContext<UserRegister>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

       
    }
}
