using KunigiMuseum.Application.Data;
using KunigiMuseum.Application.Mappings;
using KunigiMuseum.Contracts.Requests.Game;
using KunigiMuseum.Contracts.Responses.Common;
using KunigiMuseum.Contracts.Responses.Game;
using KunigiMuseum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KunigiMuseum.Application.Services;

public interface IGameService
{
    Task<ServiceResponse<GameResponse>> CreateGameAsync(CreateGameRequest request);
    Task<ServiceResponse<GameResponse>> GetGameByIdOrYearAsync(string idOrYear);
    Task<ServiceResponse<PaginatedResponse<GameResponse>>> GetPaginatedGamesAsync(int page, int pageSize);
}

public class GameService : IGameService
{
    private readonly DataContext _context;
    private readonly IUploadService _uploadService;

    public GameService(DataContext context, IUploadService uploadService)
    {
        _context = context;
        _uploadService = uploadService;
    }

    public async Task<ServiceResponse<GameResponse>> CreateGameAsync(CreateGameRequest request)
    {
        var game = new Game
        {
            Year = request.Year,
            Order = request.Order,
            MainTitle = $"{request.Order}ο Κυνήγι Θησαυρού",
            WinnerId = request.WinnerId,
            HostId = request.HostId
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        
        _uploadService.CreateFolder($"games/{game.Year.ToString()}");
        
        var response = game.MapToResponse();
        return ServiceResponse<GameResponse>.Success(response);
    }

    public async Task<ServiceResponse<GameResponse>> GetGameByIdOrYearAsync(string idOrYear)
    {
        Game? game = null;
        if (Guid.TryParse(idOrYear, out var id))
        {
            game = await _context.Games.FindAsync(id);
        }
        else if (short.TryParse(idOrYear, out var year))
        {
            game = await _context.Games.FirstOrDefaultAsync(t => t.Year == year);
        }

        if (game == null)
        {
            return ServiceResponse<GameResponse>.Failure("Το παιχνίδι δεν βρέθηκε.");
        }

        var response = game.MapToResponse();
        return ServiceResponse<GameResponse>.Success(response);
    }

    public async Task<ServiceResponse<PaginatedResponse<GameResponse>>> GetPaginatedGamesAsync(int page, int pageSize)
    {
        var query = _context.Games.AsQueryable();
    
        var totalItems = await query.CountAsync();
        var games = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var gameResponse = games.Select(t => t.MapToResponse()).ToList();
        var paginatedResponse = new PaginatedResponse<GameResponse>
        {
            Items = gameResponse,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };

        return ServiceResponse<PaginatedResponse<GameResponse>>.Success(paginatedResponse);
    }
}