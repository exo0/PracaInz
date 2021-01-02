using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PracaInz.BLL;
using System.Diagnostics.CodeAnalysis;


namespace PracaInz.DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options) { }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<NetworkDevice> NetworkDevices { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
