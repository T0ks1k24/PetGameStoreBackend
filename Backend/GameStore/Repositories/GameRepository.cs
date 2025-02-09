using GameStore.Dtos.Game;
using GameStore.Interfaces.IRepository;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories;

public class GameRepository : IGameRepository
{
    private readonly ApplicationDbContext _context;

    public GameRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    //Get All Game
    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _context.Games.Include(g => g.Category).ToListAsync();
    }

    //Get Game By Id
    public async Task<Game?> GetByIdAsync(int id)
    {
        return await _context.Games.Include(g => g.Category).FirstOrDefaultAsync(g => g.Id == id);
    }

    //Get Game For Category Id
    public async Task<IEnumerable<Game>> GetByCategoryAsync(int categoryId)
    {
        return await _context.Games.Where(g => g.CategoryId == categoryId).Include(g => g.Category).ToListAsync();
    }

    public async Task<IEnumerable<Game>> GetByFilterAsync(string? name, decimal? minPrice, decimal? maxPrice, double? evaluation)
    {
        var query = _context.Games.AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(g => g.Name.ToLower().Contains(name.ToLower()));

        if (minPrice.HasValue)
            query = query.Where(g => g.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(g => g.Price <= maxPrice.Value);

        if(evaluation.HasValue)
            query = query.Where(g => g.Evaluation >= evaluation.Value);

        return query.Include(g => g.Category).ToList();
    }

    //Create Game
    public async Task<bool> AddAsync(Game game)
    {
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();
        return true;
    }

    //Update Game
    public async Task<bool> UpdateAsync(Game game)
    {
        var categoryId = game.CategoryId;
        var category = await _context.Categories.FindAsync(categoryId);

        if (category == null)
        {
            throw new Exception($"Category with ID {categoryId} does not exist.");
        }

        _context.Games.Update(game);
        await _context.SaveChangesAsync();
        return true;
    }

    //Delete Game
    public async Task<bool> DeleteAsync(int id)
    {
        var delGame = await _context.Games.FindAsync(id);

        if (delGame != null)
        {
            _context.Remove(delGame);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }




}
