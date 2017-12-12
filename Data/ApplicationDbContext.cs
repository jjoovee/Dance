using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    public DbSet<Dance> Dances { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
        {
      base.OnModelCreating(builder);
      builder.Entity<Dance>().ToTable("Dance");
      builder.Entity<Enrollment>().ToTable("Enrollment");
      builder.Entity<Client>().ToTable("Client");
      builder.Entity<Teacher>().ToTable("Teacher");
      // Customize the ASP.NET Identity model and override the defaults if needed.
      // For example, you can rename the ASP.NET Identity table names and more.
      // Add your customizations after calling base.OnModelCreating(builder);
    }
    }
}
