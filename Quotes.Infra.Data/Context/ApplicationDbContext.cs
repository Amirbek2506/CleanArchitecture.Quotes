using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quotes.Domain.Entities;

namespace Quotes.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Quote> Quotes { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = 1,
                Name = "admin",
                NormalizedName = "admin".ToUpper()
            });
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = 2,
                Name = "custom",
                NormalizedName = "custom".ToUpper()
            });
            

            var hasher = new PasswordHasher<IdentityUser<int>>();
            builder.Entity<IdentityUser<int>>().HasData(new IdentityUser<int>
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "user@example.com",
                NormalizedEmail = "user@example.com",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "string"),
                SecurityStamp = string.Empty,
            });

            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 1,
                UserId = 1
            });

            base.OnModelCreating(builder);
        }
    }
    }
