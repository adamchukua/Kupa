using Kupa.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kupa.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Location>()
                .HasIndex(l => l.Address)
                .IsUnique()
                .HasFilter("[Address] IS NOT NULL");

            builder.Entity<Location>()
                .HasIndex(l => l.Url)
                .IsUnique()
                .HasFilter("[Url] IS NOT NULL");
        }
    }
}
