using KunigiMuseum.Contracts.Responses.Team;
using KunigiMuseum.Domain.Entities;

namespace KunigiMuseum.Application.Mappings;

public static class TeamMappings
{
    public static TeamResponse MapToResponse(this Team team)
    {
        return new TeamResponse(team.TeamId, team.Name, team.IsActive);
    }
}