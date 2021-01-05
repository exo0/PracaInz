using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PracaInz.BLL;
using System.Diagnostics.CodeAnalysis;


namespace PracaInz.DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options) { }
        // create DB for Tickets 
        public DbSet<Ticket> Tickets { get; set; }
        // create DB for Devices (including Network Devices - I'm using TPH here, below you can find more code about it)
        public DbSet<Device> Devices { get; set; }
        // create DB for Categories
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("AspNetUsers")
                .HasDiscriminator<int>("UserType")
                .HasValue<User>(0)
                .HasValue<Admin>(1)
                .HasValue<HelpDesk>(2);
        }

    }
}
