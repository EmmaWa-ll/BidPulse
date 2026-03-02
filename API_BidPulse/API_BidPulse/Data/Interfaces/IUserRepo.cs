using API_BidPulse.Data.Entities;

namespace API_BidPulse.Data.Interfaces
{
    public interface IUserRepo
    {
        // GET
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUserEmailAsync(string email);
        Task<User?> GetByUserNameAsync(string userName);

        // CREATE / UPDATE / DELETE
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
