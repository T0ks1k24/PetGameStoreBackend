using GameStore.Interfaces.IRepository;
using GameStore.Interfaces.IServices;
using GameStore.Models;

namespace GameStore.Services;

public class GameKeyService : IGameKeyService
{
    private readonly IGameKeyRepository _gameKeyRepository;

    public GameKeyService(IGameKeyRepository gameKeyRepository)
    {
        _gameKeyRepository = gameKeyRepository;
    }

    public async Task<IEnumerable<string>> GenerateKeysAsync(int gameId, int count)
    {
        var keys = new List<GameKey>();
        for (int i = 0; i < count; i++)
        {
            keys.Add(new GameKey { GameId = gameId });
        }

        await _gameKeyRepository.AddKeysAsync(keys);
        return keys.Select(k => k.Key).ToList();
    }
    public async Task<IEnumerable<string>> GetAvailableKeysAsync(int gameId)
    {
        var keys = await _gameKeyRepository.GetUnusedKeysAsync(gameId);
        return keys.Select(k=> k.Key).ToList();
    }

    public async Task<bool> ActivateKeyAsync(string key)
    {
        var gameKey = await _gameKeyRepository.GetKeyAsync(key);
        if (gameKey == null || gameKey.IsUser)
            return false;

        await _gameKeyRepository.MarkAsUsedAsync(key);
        return true;
    }
}
