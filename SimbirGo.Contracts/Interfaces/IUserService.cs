using SimbirGo.Contracts.Commands.SimbirGoCommands;
using SimbirGo.Contracts.DTO;

namespace SimbirGo.Contracts.Interfaces;

public interface IUserService
{
    Task<UserDTO> CreateUser(string notificationID, UserCreateCommand notification, CancellationToken cancellationToken = default);
    Task<UserDTO> GetUserById(string notificationID, CancellationToken cancellationToken = default);
    Task<ICollection<UserDTO>> GetAllUsers(CancellationToken cancellationToken = default);
    Task<UserDTO> UpdateUser(string notificationID, UserUpdateCommand notification, CancellationToken cancellationToken = default);
    Task DeleteUser(string notificationID, CancellationToken cancellationToken = default);
}