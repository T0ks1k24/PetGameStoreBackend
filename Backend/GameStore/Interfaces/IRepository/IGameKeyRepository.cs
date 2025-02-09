using GameStore.Models;

namespace GameStore.Interfaces.IRepository
{
    public interface IGameKeyRepository
    {
        Task<IEnumerable<GameKey>> GetUnusedKeysAsync(int gameId);
        Task<GameKey?> GetKeyAsync(string key);
        Task AddKeysAsync(IEnumerable<GameKey> keys);
        Task MarkAsUsedAsync(string key);
    }
}
