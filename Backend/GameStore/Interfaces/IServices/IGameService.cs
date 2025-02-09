using GameStore.Dtos.Game;

namespace GameStore.Interfaces.IServices
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetGamesAsync(FilterDto filter);
        Task<GameDto> GetGameDetailsAsync(int id);
        Task<IEnumerable<GameDto>> GetGameByCategoryAsync(int categoryId);
        Task<bool> AddGameAsync(CreateGameDto gameDto);
        Task<bool> UpdateGameAsync(int id, UpdateGameDto gameDto);
        Task<bool> DeleteGameAsync(int id);
    }
}
