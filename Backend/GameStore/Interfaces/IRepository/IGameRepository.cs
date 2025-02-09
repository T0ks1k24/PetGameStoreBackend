using GameStore.Dtos.Game;
using GameStore.Models;

namespace GameStore.Interfaces.IRepository;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetAllAsync();
    Task<Game> GetByIdAsync(int id);
    Task<IEnumerable<Game>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Game>> GetByFilterAsync(string? name, decimal? minPrice, decimal? maxPrice, double? evaluation);
    Task<bool> AddAsync(Game game);
    Task<bool> UpdateAsync(Game game);
    Task<bool> DeleteAsync(int id);
}
