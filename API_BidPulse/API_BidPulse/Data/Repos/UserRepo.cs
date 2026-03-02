using API_BidPulse.Data.Entities;
using API_BidPulse.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API_BidPulse.Data.Repos
{
    public class UserRepo : IUserRepo
    {

        private readonly BidPulseDbContext _context;
        public UserRepo(BidPulseDbContext context)
        {
            _context = context;
        }



        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);
        }

    
        public async Task<User?> GetByUserEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

 
        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Name == userName);
        }

        public async Task UpdateUserAsync(User user)
        {
            var original = await _context.Users.SingleOrDefaultAsync(u => u.UserId == user.UserId);
            if (original == null)
            {
                return;
            }

            _context.Entry(original).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }
    }
}
