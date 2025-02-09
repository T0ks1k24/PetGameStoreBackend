using GameStore.Interfaces.IServices;
using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using GameStore.Dtos.Game;
using GameStore.Interfaces.IRepository;
using GameStore.Dtos.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.PasswordHash,
            Role = user.Role,
        };
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return false;

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Success;
    }

    public async Task<bool> RegisterAsync(RegisterUserDto userDto)
    {
        var user = new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            PasswordHash = _passwordHasher.HashPassword(null, userDto.Password) // Хешуємо пароль
        };

        return await _userRepository.RegisterAsync(user);
    }

    public async Task<bool> UpdateProfileAsync(int userId, UpdateUserDto userDto)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return false;

        user.Name = userDto.Name;
        user.Email = userDto.Email;

        if (!string.IsNullOrWhiteSpace(userDto.Password))
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.Password); // Хешуємо новий пароль
        }

        return await _userRepository.UpdateProfileAsync(user);
    }

    public async Task<bool> DeleteAccountAsync(int userId)
    {
        return await _userRepository.DeleteAccountAsync(userId);
    }
}
