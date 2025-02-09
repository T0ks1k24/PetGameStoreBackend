using GameStore.Dtos.Game;
using GameStore.Interfaces.IRepository;
using GameStore.Interfaces.IServices;
using GameStore.Models;

namespace GameStore.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<IEnumerable<GameDto>> GetGamesAsync(FilterDto filter)
    {
        var games = await _gameRepository.GetByFilterAsync(filter.Name, filter.MinPrice, filter.MaxPrice, filter.Evaluation);
        return games.Select(g => new GameDto
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
            Price = g.Price,
            Evaluation = g.Evaluation,
            UrlImage = g.UrlImage,
            CategoryName = g.Category.Name
        }).ToList();
    }

    public async Task<GameDto?> GetGameDetailsAsync(int id)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        if (game == null) return null;

        return new GameDto
        {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            Price = game.Price,
            Evaluation = game.Evaluation,
            UrlImage = game.UrlImage,
            CategoryName = game.Category.Name
        };
    }

    public async Task<IEnumerable<GameDto>> GetGameByCategoryAsync(int categoryId)
    {
        var games = await _gameRepository.GetByCategoryAsync(categoryId);

        return games.Select(game => new GameDto
        {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            Price = game.Price,
            Evaluation = game.Evaluation,
            UrlImage = game.UrlImage,
            CategoryName = game.Category?.Name
        }).ToList();
    }

    public async Task<bool> AddGameAsync(CreateGameDto gameDto)
    {
        var game = new Game
        {
            Name = gameDto.Name,
            Description = gameDto.Description,
            Price = gameDto.Price,
            Evaluation = gameDto.Evaluation,
            UrlImage = gameDto.UrlImage,
            CategoryId = gameDto.CategoryId
        };

        return await _gameRepository.AddAsync(game);
    }

    public async Task<bool> UpdateGameAsync(int id,UpdateGameDto gameDto)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        if (game == null) return false;

        game.Name = gameDto.Name;
        game.Description = gameDto.Description;
        game.Price = gameDto.Price;
        game.Evaluation = gameDto.Evaluation;
        game.UrlImage = gameDto.UrlImage;
        game.CategoryId = gameDto.CategoryId;

        return await _gameRepository.UpdateAsync(game);
    }

    public async Task<bool> DeleteGameAsync(int id)
    {
        return await _gameRepository.DeleteAsync(id);
    }
}
