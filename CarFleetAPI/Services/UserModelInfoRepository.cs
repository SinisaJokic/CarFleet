using CarFleetAPI.DbContexts;
using CarFleetAPI.Entities;
using CarFleetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarFleetAPI.Services
{
    public class UserModelInfoRepository : IUserModelInfoRepository
    {
        private readonly VehicleInfoContext _context;

        public UserModelInfoRepository(VehicleInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<UserModel>> GetUserModelsAsync()
        {
            return await _context.Users.OrderBy(c => c.PkUser).ToListAsync();
        }

        public async Task<UserModel?> GetUserModelAsync(int pkUser)
        {
           
            return await _context.Users
                  .Where(c => c.PkUser == pkUser).FirstOrDefaultAsync();
        }

        public async Task<bool> UserModelExistsAsync(int pkUser)
        {
            return await _context.Users.AnyAsync(c => c.PkUser == pkUser);
        }
        public async Task AddUserModelAsync(UserModel userModel)
        {
            _context.Users.Add(userModel);
        }

     
        public void DeleteUserModel(UserModel userModel)
        {
            _context.Users.Remove(userModel);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
        public async Task<UserModel?> GetUserPassAsync(string userName, string password)
        {

            return await _context.Users
                  .Where(c => c.UserName == userName && c.Password==password).FirstOrDefaultAsync();
        }
    }
}
