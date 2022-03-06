using CarFleetAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarFleetAPI.DbContexts

{
    public class VehicleInfoContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Driver> Drivers { get; set; } = null!;
        public DbSet<VehicleAssign> VehicleAssign { get; set; } = null!;

        public VehicleInfoContext(DbContextOptions<VehicleInfoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasData(
               new Vehicle("Mercedes")
               {
                   Id = 1,
                   RegistrationNumber="ST-111-AA"
               },
               new Vehicle("Volovo")
               {
                   Id = 2,
                   RegistrationNumber="ST-222-BB"
               });
            base.OnModelCreating(modelBuilder);
        }
    }
}
