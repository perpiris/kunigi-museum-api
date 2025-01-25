using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KunigiMuseum.Domain.Entities;

public class Team
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid TeamId { get; set; }

    [MaxLength(150)] public required string Slug { get; set; }

    [MaxLength(150)] public required string Name { get; set; }

    public bool IsActive { get; set; }
    
    public short? FoundedYear { get; set; }

    public string? Description { get; set; }

    [MaxLength(150)] public string? WebsiteUrl { get; set; }

    [MaxLength(150)] public string? FacebookUrl { get; set; }

    [MaxLength(150)] public string? InstagramUrl { get; set; }

    [MaxLength(150)] public string? YoutubeUrl { get; set; }

    public virtual ICollection<Game> HostedGames { get; set; } = new List<Game>();

    public virtual ICollection<Game> WonGames { get; set; } = new List<Game>();
}