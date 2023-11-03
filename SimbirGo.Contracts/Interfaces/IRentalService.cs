using SimbirGo.Contracts.Commands.SimbirGoCommands;
using SimbirGo.Contracts.DTO;

namespace SimbirGo.Contracts.Interfaces;

public interface IRentalService
{
    Task<RentalDTO> CreateRental(string notificationID, RentalCreateCommand notification, CancellationToken cancellationToken = default);
    Task<RentalDTO> GetRentalById(string notificationID, CancellationToken cancellationToken = default);
    Task<ICollection<RentalDTO>> GetAllRentals(CancellationToken cancellationToken = default);
    Task<RentalDTO> UpdateRental(string notificationID, RentalUpdateCommand notification, CancellationToken cancellationToken = default);
    Task DeleteRental(string notificationID, CancellationToken cancellationToken = default);
}