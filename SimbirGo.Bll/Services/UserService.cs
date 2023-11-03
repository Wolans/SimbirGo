using Microsoft.EntityFrameworkCore;

using SimbirGo.Bll.DbConfiguration;
using SimbirGo.Contracts.Commands.SimbirGoCommands;
using SimbirGo.Contracts.DTO;
using SimbirGo.Contracts.Interfaces;
using SimbirGo.Contracts.Models;

namespace SimbirGo.Bll;

public class UserService : IUserService
{
    private readonly SimbirGoDbContext _dbContext;

    public UserService(SimbirGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDTO> CreateUser(string userID, UserCreateCommand user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        User newUser = new(userID,
            user.UserName,
            user.PasswordHash,
            user.Email,
            user.Role,
            user.CreatedAt,
            user.UpdatedAt);

        await _dbContext.Users.AddAsync(newUser, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ModelToDTO(newUser);
    }

    public async Task<UserDTO> GetUserById(string userID, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existingUser = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(t => t.UserID == userID, cancellationToken)
            ?? throw new ArgumentException("Пользователь не найден.");

        return ModelToDTO(existingUser);
    }

    public async Task<ICollection<UserDTO>> GetAllUsers(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _dbContext.Users.AsNoTracking()
        .Select(user => ModelToDTO(user))
        .ToListAsync(cancellationToken);
    }
    public async Task<UserDTO> UpdateUser(string userID, UserUpdateCommand updateUserData, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(t => t.UserID == userID, cancellationToken)
            ?? throw new ArgumentException("Пользователь не найден.");

        userToUpdate.UserName = updateUserData.UserName;
        userToUpdate.PasswordHash = updateUserData.PasswordHash;
        userToUpdate.Email = updateUserData.Email;
        userToUpdate.Role = updateUserData.Role;
        userToUpdate.CreatedAt = updateUserData.CreatedAt;
        userToUpdate.UpdatedAt = updateUserData.UpdatedAt;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return ModelToDTO(userToUpdate);
    }

    public async Task DeleteUser(string userID, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var userToDelete = await _dbContext.Users.FirstOrDefaultAsync(t => t.UserID == userID, cancellationToken) 
            ?? throw new ArgumentException("Пользователь не найден.");
        
        _dbContext.Users.Remove(userToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static UserDTO ModelToDTO(User user) => new(
        user.UserID,
        user.UserName,
        user.PasswordHash,
        user.Email,
        user.Role,
        user.CreatedAt,
        user.UpdatedAt);

}