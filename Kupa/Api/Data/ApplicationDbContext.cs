using AutoMapper;
using Kupa.Api.Enums;
using Kupa.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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

        public DbSet<City> Cities { get; set; }

        public DbSet<Category> Categories { get; set; }

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

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Музика" },
                new Category { Id = 2, Name = "Театр" },
                new Category { Id = 3, Name = "Кіно" },
                new Category { Id = 4, Name = "Спорт" },
                new Category { Id = 5, Name = "Література" },
                new Category { Id = 6, Name = "Наука" },
                new Category { Id = 7, Name = "Технології" },
                new Category { Id = 8, Name = "Кулінарія" },
                new Category { Id = 9, Name = "Мистецтво" },
                new Category { Id = 10, Name = "Історія" },
                new Category { Id = 11, Name = "Подорожі" },
                new Category { Id = 12, Name = "Мода" },
                new Category { Id = 13, Name = "Навчання" },
                new Category { Id = 14, Name = "Релігія" },
                new Category { Id = 15, Name = "Політика" },
                new Category { Id = 16, Name = "Дитячі заходи" },
                new Category { Id = 17, Name = "Природа" }
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
