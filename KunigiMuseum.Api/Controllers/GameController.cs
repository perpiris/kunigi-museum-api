using KunigiMuseum.Application.Services;
using KunigiMuseum.Contracts.Requests.Game;
using Microsoft.AspNetCore.Mvc;

namespace KunigiMuseum.Api.Controllers;

[ApiController]
[Route("games")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGame(CreateGameRequest request)
    {
        var result = await _gameService.CreateGameAsync(request);
        if (!result.IsSuccess || result.Data is null)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetGameByIdOrYear),
            new { idOrYear = result.Data.GameId },
            result);
    }

    [HttpGet("{idOrYear}")]
    public async Task<IActionResult> GetGameByIdOrYear(string idOrYear)
    {
        var result = await _gameService.GetGameByIdOrYearAsync(idOrYear);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPaginatedGames(int page = 1, int pageSize = 10)
    {
        var result = await _gameService.GetPaginatedGamesAsync(page, pageSize);
        return Ok(result);
    }
}