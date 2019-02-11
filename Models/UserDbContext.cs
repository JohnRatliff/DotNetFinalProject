using Microsoft.EntityFrameworkCore;
 
namespace DotNetFinalProject.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> User { get;set; }

    }
}
