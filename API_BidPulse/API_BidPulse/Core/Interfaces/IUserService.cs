using API_BidPulse.Data.DTO.User;
using API_BidPulse.Data.Entities;

namespace API_BidPulse.Core.Interfaces
{
    public interface IUserService
    {
        Task<User?> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(RegisterDTO dto);

        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);

        Task<bool> UpdateUserAsync(int id, UserUpdateDTO dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
