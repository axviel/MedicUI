using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicDev.Models.Authentication
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<PublicDeclaration.PublicDeclaration> PublicDeclaration { get; set; }
        public DbSet<PublicDeclaration.PublicDeclarationSignature> PublicDeclarationSignature { get; set; }
    }
}
