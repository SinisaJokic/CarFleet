using CarFleetAPI.DbContexts;
using CarFleetAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarFleetAPI.Services
{
    public class VehicleAssignRepository : IVehicleAssignRepository
    {
        private readonly VehicleInfoContext _context;

        public VehicleAssignRepository(VehicleInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<VehicleAssign>> GetVehicleAssignAsync()
        {
            return await _context.VehicleAssign.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<VehicleAssign?> GetVehicleAssignAsync(int vehicleAssignId)
        {
           
            return await _context.VehicleAssign
                  .Where(c => c.Id == vehicleAssignId).FirstOrDefaultAsync();
        }

        public async Task<bool> VehicleAssignExistsAsync(int vehicleAssignId)
        {
            return await _context.VehicleAssign.AnyAsync(c => c.Id == vehicleAssignId);
        }
        public async Task AddVehicleAssignAsync(VehicleAssign vehicleAssign)
        {
            _context.VehicleAssign.Add(vehicleAssign);
        }

     
        public void DeleteVehicleAssign(VehicleAssign vehicleAssign)
        {
            _context.VehicleAssign.Remove(vehicleAssign);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
