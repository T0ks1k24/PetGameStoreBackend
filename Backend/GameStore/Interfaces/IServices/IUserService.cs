using GameStore.Dtos.User;

namespace GameStore.Interfaces.IServices
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(int id);
        Task<bool> RegisterAsync(RegisterUserDto userDto);
        Task<bool> LoginAsync(string email, string password);
        Task<bool> UpdateProfileAsync(int userId, UpdateUserDto userDto);
        Task<bool> DeleteAccountAsync(int userId);
    }
}
