using SimbirGo.Contracts.Models;

namespace SimbirGo.Contracts.Commands.SimbirGoCommands;

public record UserUpdateCommand(
    string UserName,
    string PasswordHash,
    string Email,
    string Role,
    DateTime CreatedAt,
    DateTime UpdatedAt);