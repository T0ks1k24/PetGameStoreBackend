using GameStore.Dtos;
using GameStore.Models;

namespace GameStore.Interfaces.IRepository;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User> GetByEmailAsync(string email);
    Task<bool> RegisterAsync(User user);
    Task<bool> UpdateProfileAsync(User user);
    Task<bool> DeleteAccountAsync(int id);
}
