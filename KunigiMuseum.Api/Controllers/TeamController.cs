using KunigiMuseum.Application.Services;
using KunigiMuseum.Contracts.Requests.Team;
using Microsoft.AspNetCore.Mvc;

namespace KunigiMuseum.Api.Controllers;

[ApiController]
[Route("teams")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeam(CreateTeamRequest request)
    {
        var result = await _teamService.CreateTeamAsync(request);
        if (!result.IsSuccess || result.Data is null)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetTeamByIdOrSlug),
            new { idOrSlug = result.Data.TeamId },
            result.Data);
    }

    [HttpGet("{idOrSlug}")]
    public async Task<IActionResult> GetTeamByIdOrSlug(string idOrSlug)
    {
        var result = await _teamService.GetByIdOrSlugAsync(idOrSlug);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginatedTeams(int page = 1, int pageSize = 10)
    {
        var result = await _teamService.GetPaginatedTeamsAsync(page, pageSize);
        return Ok(result.Data);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTeam(Guid id, UpdateTeamRequest request)
    {
        var result = await _teamService.UpdateTeamAsync(id, request);
        if (!result.IsSuccess || result.Data is null)
        {
            return BadRequest(result);
        }

        return Ok(result.Data);
    }
}