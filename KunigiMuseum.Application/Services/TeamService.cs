using KunigiMuseum.Application.Data;
using KunigiMuseum.Application.Mappings;
using KunigiMuseum.Contracts.Requests.Team;
using KunigiMuseum.Contracts.Responses.Common;
using KunigiMuseum.Contracts.Responses.Team;
using KunigiMuseum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KunigiMuseum.Application.Services;

public interface ITeamService
{
    Task<ServiceResponse<TeamResponse>> CreateTeamAsync(CreateTeamRequest request);
    Task<ServiceResponse<TeamResponse>> GetByIdOrSlugAsync(string idOrSlug);
    Task<ServiceResponse<PaginatedResponse<TeamResponse>>> GetPaginatedTeamsAsync(int page, int pageSize);
}

public class TeamService : ITeamService
{
    private readonly DataContext _context;

    public TeamService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<TeamResponse>> CreateTeamAsync(CreateTeamRequest request)
    {
        var slug = request.Name;
        var exists = await _context.Teams.AnyAsync(x => x.Slug == slug);
        if (exists)
        {
            return ServiceResponse<TeamResponse>.Failure("Υπάρχει ήδη ομάδα με αυτό το όνομα.");
        }

        var team = new Team
        {
            Slug = slug,
            Name = request.Name,
            IsActive = request.IsActive,
        };

        _context.Teams.Add(team);
        await _context.SaveChangesAsync();

        var response = team.MapToResponse();
        return ServiceResponse<TeamResponse>.Success(response);
    }
    
    public async Task<ServiceResponse<TeamResponse>> GetByIdOrSlugAsync(string idOrSlug)
    {
        Team? team = null;
        if (int.TryParse(idOrSlug, out var id))
        {
            team = await _context.Teams.FindAsync(id);
        }
        else
        {
            team = await _context.Teams.FirstOrDefaultAsync(t => t.Slug == idOrSlug);
        }

        if (team == null)
        {
            return ServiceResponse<TeamResponse>.Failure("Η ομάδα δεν βρέθηκε.");
        }

        var response = team.MapToResponse();
        return ServiceResponse<TeamResponse>.Success(response);
    }
    
    public async Task<ServiceResponse<PaginatedResponse<TeamResponse>>> GetPaginatedTeamsAsync(int page, int pageSize)
    {
        var query = _context.Teams.AsQueryable();
    
        var totalItems = await query.CountAsync();
        var teams = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var teamResponses = teams.Select(t => t.MapToResponse()).ToList();
        var paginatedResponse = new PaginatedResponse<TeamResponse>
        {
            Items = teamResponses,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };

        return ServiceResponse<PaginatedResponse<TeamResponse>>.Success(paginatedResponse);
    }
}