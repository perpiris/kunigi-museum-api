namespace KunigiMuseum.Contracts.Responses.Team;

public record TeamResponse(
    Guid TeamId, 
    string Name,
    string Slug,
    bool IsActive, 
    short? FoundedYear, 
    string? Description, 
    string? WebsiteUrl, 
    string? FacebookUrl, 
    string? InstagramUrl, 
    string? YoutubeUrl);