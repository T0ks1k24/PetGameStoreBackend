using System.Collections.Generic;

namespace GameStore.Interfaces.IServices
{
    public interface IGameKeyService
    {
        Task<IEnumerable<string>> GenerateKeysAsync(int gameId, int count);
        Task<IEnumerable<string>> GetAvailableKeysAsync(int gameId);
        Task<bool> ActivateKeyAsync(string key);
    }
}
