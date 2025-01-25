namespace KunigiMuseum.Contracts.Requests.Team;

public record UpdateTeamRequest(
    bool IsActive,
    short FoundedYear,
    string? Description,
    string? WebsiteUrl,
    string? FacebookUrl,
    string? InstagramUrl,
    string? YoutubeUrl);