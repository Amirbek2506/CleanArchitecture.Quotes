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

            base.OnModelCreating(builder);
        }
    }
    }
