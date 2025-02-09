using GameStore.Dtos.Game;
using GameStore.Interfaces;
using GameStore.Interfaces.IServices;
using GameStore.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGames()
    {
        var games = await _gameService.GetGamesAsync(new FilterDto());
        return Ok(games);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGameById([FromRoute] int id)
    {
        var game = await _gameService.GetGameDetailsAsync(id);
        if(game == null) return NotFound();
        return Ok(game);
    }

    
    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetGamesByCategory([FromRoute] int categoryId)
    {
        var games = await _gameService.GetGameByCategoryAsync(categoryId);
        return Ok(games);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetGamesByFilter([FromQuery] string? name, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] double? evaluation)
    {
        var games = await _gameService.GetGamesAsync(new FilterDto { Name = name, MinPrice = minPrice, MaxPrice = maxPrice, Evaluation = evaluation });
        return Ok(games);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameDto gameDto)
    {
        await _gameService.AddGameAsync(gameDto);
        return CreatedAtAction(nameof(GetAllGames), new { });
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateGame([FromRoute] int id, [FromBody] UpdateGameDto gameDto)
    {
        return Ok(await _gameService.UpdateGameAsync(id, gameDto));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteGame([FromRoute] int id)
    {
        bool delGame = await _gameService.DeleteGameAsync(id);

        if (delGame == false)
            return BadRequest("Not game remove!");

        return Ok("Game remove!");
    }
}
