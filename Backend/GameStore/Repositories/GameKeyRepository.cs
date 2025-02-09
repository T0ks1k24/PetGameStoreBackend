using GameStore.Interfaces.IRepository;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories
{
    public class GameKeyRepository : IGameKeyRepository
    {
        private readonly ApplicationDbContext _context;

        public GameKeyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GameKey>> GetUnusedKeysAsync(int gameId)
        {
            return await _context.GameKeys
                .Where(k => k.GameId == gameId && !k.IsUser)
                .ToListAsync();
        }

        public async Task AddKeysAsync(IEnumerable<GameKey> keys)
        {
            await _context.GameKeys.AddRangeAsync(keys);
            await _context.SaveChangesAsync();
        }

        public async Task<GameKey?> GetKeyAsync(string key)
        {
            return await _context.GameKeys.FirstOrDefaultAsync(k => k.Key == key);
        }

        

        public async Task MarkAsUsedAsync(string key)
        {
            var gameKey = await GetKeyAsync(key);
            if (gameKey != null)
            {
                gameKey.IsUser = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
