using SimbirGo.Contracts.Models;

namespace SimbirGo.Contracts.DTO;

public record UserDTO(
    string UserID, 
    string UserName, 
    string PasswordHash, 
    string Email, 
    string Role, 
    DateTime CreatedAt, 
    DateTime UpdatedAt);
