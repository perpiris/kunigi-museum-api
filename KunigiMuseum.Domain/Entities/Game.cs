using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KunigiMuseum.Domain.Entities;

public class Game
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid GameId { get; set; }

    public short Year { get; set; }

    public short Order { get; set; }

    [MaxLength(150)] public required string MainTitle { get; set; }

    [MaxLength(150)] public string? SubTitle { get; set; }

    public string? Description { get; set; }

    public required Guid HostId { get; set; }

    public required Guid WinnerId { get; set; }

    [ForeignKey("HostId")] public virtual Team Host { get; set; }

    [ForeignKey("WinnerId")] public virtual Team Winner { get; set; }
}