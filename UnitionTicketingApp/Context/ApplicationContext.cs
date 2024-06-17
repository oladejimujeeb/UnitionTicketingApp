using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UnitionTicketingApp.Entities;

namespace UnitionTicketingApp.Context
{
    public class ApplicationContext:IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
        }
        public DbSet<Bug> Bugs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = "QA", NormalizedName = "QA" },
            new IdentityRole { Name = "RD", NormalizedName = "RD" },
            new IdentityRole { Name = "PM", NormalizedName = "PM" },
            new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" } );
        }
    }
}
