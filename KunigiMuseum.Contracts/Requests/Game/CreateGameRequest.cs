namespace KunigiMuseum.Contracts.Requests.Game;

public record CreateGameRequest(short Year, short Order, Guid HostId, Guid WinnerId);