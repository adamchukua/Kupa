using AutoMapper;
using Kupa.Api.Enums;
using Kupa.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kupa.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventSurveyQuestion> EventSurveyQuestions { get; set; }

        public DbSet<EventSurveyAnswer> EventSurveyAnswers { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<EventStatus> EventStatuses { get; set; }

        public DbSet<EventComment> EventComments { get; set; }

        public DbSet<EventRegistration> EventRegistrations { get; set; }

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

            builder.Entity<EventSurveyAnswer>()
                .HasIndex(e => new { e.EventSurveyQuestionId, e.CreatedByUserId })
                .IsUnique();

            builder.Entity<EventStatus>().HasData(
                new EventStatus { Id = EventStatusId.Pending, Name = "Pending" },
                new EventStatus { Id = EventStatusId.Active, Name = "Active" },
                new EventStatus { Id = EventStatusId.Completed, Name = "Completed" },
                new EventStatus { Id = EventStatusId.Cancelled, Name = "Cancelled" }
            );

            builder.Entity<Event>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<EventComment>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
