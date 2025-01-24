using System.ComponentModel.DataAnnotations;

namespace KunigiMuseum.Domain.Entities;

public class Team
{
    [Key]
    public int TeamId { get; set; }

    [MaxLength(150)]
    public required string Slug { get; set; }
    
    [MaxLength(150)]
    public required string Name { get; set; }
    
    public bool IsActive { get; set; }
    
    public string? Description { get; set; }
    
    [MaxLength(150)]
    public string? WebsiteUrl { get; set; }
    
    [MaxLength(150)]
    public string? FacebookUrl { get; set; }
    
    [MaxLength(150)]
    public string? InstagramUrl { get; set; }
    
    [MaxLength(150)]
    public string? YoutubeUrl { get; set; }
}