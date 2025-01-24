using KunigiMuseum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KunigiMuseum.Application.Data;

public class DataContext : DbContext
{
    public DbSet<Team> Teams { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
}