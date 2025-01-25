namespace KunigiMuseum.Contracts.Responses.Game;

public record GameResponse(Guid GameId, short Year, short Order, string MainTitle);