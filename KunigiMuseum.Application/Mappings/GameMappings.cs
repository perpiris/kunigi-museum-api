using KunigiMuseum.Contracts.Responses.Game;
using KunigiMuseum.Domain.Entities;

namespace KunigiMuseum.Application.Mappings;

public static class GameMappings
{
    public static GameResponse MapToResponse(this Game game)
    {
        return new GameResponse(game.GameId, game.Year, game.Order, game.MainTitle);
    }
}