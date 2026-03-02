using API_BidPulse.Core.Interfaces;
using API_BidPulse.Data;
using API_BidPulse.Data.DTO.User;
using API_BidPulse.Data.Entities;
using API_BidPulse.Data.Interfaces;

namespace API_BidPulse.Core.Services
{

    public class UserService : IUserService
    {

        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }




        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepo.GetByIdAsync(id);
        }

        public async Task<bool> RegisterAsync(RegisterDTO dto)
        {
            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
                return false;

            var email = dto.Email.Trim();

            if (await _userRepo.GetByUserEmailAsync(email) != null)
                return false;

            await _userRepo.AddUserAsync(new User
            {
                Name = dto.Name.Trim(),
                Email = email,
                Password = dto.Password
            });

            return true;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = await _userRepo.GetByUserEmailAsync(email.Trim());
            return user?.Password == password ? user : null;
        }


        public async Task<bool> UpdateUserAsync(int id, UserUpdateDTO dto)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Name)) user.Name = dto.Name.Trim();
            if (!string.IsNullOrWhiteSpace(dto.Email)) user.Email = dto.Email.Trim();
            if (!string.IsNullOrWhiteSpace(dto.Password)) user.Password = dto.Password;

            await _userRepo.UpdateUserAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if (await _userRepo.GetByIdAsync(id) == null)
                return false;

            await _userRepo.DeleteUserAsync(id);
            return true;
        }


    }
}
