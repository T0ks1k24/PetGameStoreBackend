using GameStore.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameKeyController : ControllerBase
    {
        private readonly IGameKeyService _gameKeyService;

        public GameKeyController(IGameKeyService gameKeyService)
        {
            _gameKeyService = gameKeyService;
        }

        [HttpPost("{gameId}/generate")]
        public async Task<IActionResult> GenerateKeys(int gameId, int count)
        {
            var keys = await _gameKeyService.GenerateKeysAsync(gameId, count);
            return Ok(keys);
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetAvailableKeys(int gameId)
        {
            var keys = await _gameKeyService.GetAvailableKeysAsync(gameId);
            return Ok(keys);
        }

        [HttpPost("activate/{key}")]
        public async Task<IActionResult> ActivateKey(string key)
        {
            var success = await _gameKeyService.ActivateKeyAsync(key);
            if (!success) return BadRequest("Invalid or already used key");
            return Ok("Key activated");
        }
    }
}
