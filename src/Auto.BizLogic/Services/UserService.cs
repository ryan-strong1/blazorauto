using Auto.BizLogic.Mapping;
using Auto.BizLogic.Models.Dto;
using Auto.Data.Repositories;

namespace Auto.BizLogic
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();

        Task<UserDto?> GetUserbyEmail(string email);

        Task<UserDto?> GetUserbyId(int id);

        Task<bool> DeleteUserByIdAsync(int id);
    }

    public class UserService : IUserService
    {
        protected readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(s => s.ToDto());
        }

        public async Task<UserDto?> GetUserbyId(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user?.ToDto();
        }

        public async Task<UserDto?> GetUserbyEmail(string email)
        {
            var user = await _userRepository.GetUserbyEmail(email);
            return user?.ToDto();
        }

        public async Task<bool> DeleteUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            await _userRepository.DeleteAsync(id);

            return true;
        }
    }
}