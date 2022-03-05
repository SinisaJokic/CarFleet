using CarFleetAPI.DbContexts;
using CarFleetAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarFleetAPI.Services
{
    public class VehicleInfoRepository : IVehicleInfoRepository
    {
        private readonly VehicleInfoContext _context;

        public VehicleInfoRepository(VehicleInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            return await _context.Vehicles.OrderBy(c => c.Model).ToListAsync();
        }

        public async Task<Vehicle?> GetVehicleAsync(int vehicleId)
        {
           
            return await _context.Vehicles
                  .Where(c => c.Id == vehicleId).FirstOrDefaultAsync();
        }

        public async Task<bool> VehicleExistsAsync(int vehicleId)
        {
            return await _context.Vehicles.AnyAsync(c => c.Id == vehicleId);
        }
        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
        }

     
        public void DeleteVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
