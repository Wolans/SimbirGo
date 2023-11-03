using SimbirGo.Contracts.Commands.SimbirGoCommands;
using SimbirGo.Contracts.DTO;

namespace SimbirGo.Contracts.Interfaces;

public interface ITransportService
{
    Task<TransportDTO> CreateTransport(string notificationID, TransportCreateCommand notification, CancellationToken cancellationToken = default);
    Task<TransportDTO> GetTransportById(string notificationID, CancellationToken cancellationToken = default);
    Task<ICollection<TransportDTO>> GetAllTransports(CancellationToken cancellationToken = default);
    Task<TransportDTO> UpdateTransport(string notificationID, TransportUpdateCommand notification, CancellationToken cancellationToken = default);
    Task DeleteTransport(string notificationID, CancellationToken cancellationToken = default);
}