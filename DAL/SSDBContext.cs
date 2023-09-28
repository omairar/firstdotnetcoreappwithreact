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
        public SSDBContext(DbContextOptions<SSDBContext> options) : base(options)
        {
            Database.Migrate();
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //optionsBuilder.UseSqlServer(@"Server=DESKTOP-MSQQ9TJ\SQLEXPRESS01;Database=SSDb_8;Trusted_Connection=True;");
        //}

        public DbSet<Story>? Stories { get; set; }
        public DbSet<Category>? Categories { get; set; }
    }
}