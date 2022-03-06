using CarFleetAPI.DbContexts;
using CarFleetAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarFleetAPI.Services
{
    public class DriverInfoRepository : IDriverInfoRepository
    {
        private readonly VehicleInfoContext _context;

        public DriverInfoRepository(VehicleInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Driver>> GetDriversAsync()
        {
            return await _context.Drivers.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Driver?> GetDriverAsync(int driverId)
        {
           
            return await _context.Drivers
                  .Where(c => c.Id == driverId).FirstOrDefaultAsync();
        }

        public async Task<bool> DriverExistsAsync(int driverId)
        {
            return await _context.Drivers.AnyAsync(c => c.Id == driverId);
        }
        public async Task AddDriverAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
        }

     
        public void DeleteDriver(Driver driver)
        {
            _context.Drivers.Remove(driver);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
