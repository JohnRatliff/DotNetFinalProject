using Microsoft.EntityFrameworkCore;
 
namespace DotNetFinalProject.Models
{
    public class DotNetFinalProjectDbContext : DbContext
    {
        public DotNetFinalProjectDbContext(DbContextOptions<DotNetFinalProjectDbContext> options) : base(options) { }

        public DbSet<Contact> Contact { get;set; }
        public DbSet<PaymentTerm> PaymentTerm { get;set; }
        public DbSet<Uom> Uom { get;set; }
        public DbSet<UomConversion> UomConversion { get;set; }
        public DbSet<UomType> UomType { get;set; }
        public DbSet<UomView> UomView { get;set; }
        public DbSet<User> User { get;set; }

    }
}
