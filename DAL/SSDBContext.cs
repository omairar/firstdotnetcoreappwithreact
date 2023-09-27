using BOL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    // Step 1. Add Project Reference

    // Step 2: Install EF Core Package
    //Microsoft.EntityFrameworkCore.SqlServer
    //Microsoft.EntityFrameworkCore.Tools
    //Microsoft.AspNetCore.Identity.EntityFrameworkCore

    public class SSDBContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=T480S;Database=SSDB;Trusted_Connection=true;");
        }

        public DbSet<Story>? Stories { get; set; }
    }
}