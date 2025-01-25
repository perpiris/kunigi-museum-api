using KunigiMuseum.Application.Data;
using KunigiMuseum.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KunigiMuseum.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IUploadService, UploadService>();
        
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        
        return services;
    }
}