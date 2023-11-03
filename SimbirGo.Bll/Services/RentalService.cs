using Microsoft.EntityFrameworkCore;

using SimbirGo.Bll.DbConfiguration;
using SimbirGo.Contracts.Commands.SimbirGoCommands;
using SimbirGo.Contracts.DTO;
using SimbirGo.Contracts.Interfaces;
using SimbirGo.Contracts.Models;

namespace SimbirGo.Bll;

public class RentalService : IRentalService
{
    private readonly SimbirGoDbContext _dbContext;

    public RentalService(SimbirGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RentalDTO> CreateRental(string rentalID, RentalCreateCommand rental, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Rental newRental = new(rentalID,
            rental.UserID,
            rental.TransportID,
            rental.TimeStart,
            rental.TimeEnd,
            rental.PriceOfUnit,
            rental.PriceType,
            rental.FinalPrice);

        await _dbContext.Rentals.AddAsync(newRental, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ModelToDTO(newRental);
    }

    public async Task<RentalDTO> GetRentalById(string rentalID, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existingRental = await _dbContext.Rentals.AsNoTracking().FirstOrDefaultAsync(t => t.RentalID == rentalID, cancellationToken)
            ?? throw new ArgumentException("Аренда не найдена.");

        return ModelToDTO(existingRental);
    }

    public async Task<ICollection<RentalDTO>> GetAllRentals(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _dbContext.Rentals.AsNoTracking()
        .Select(rental => ModelToDTO(rental))
        .ToListAsync(cancellationToken);
    }

    public async Task<RentalDTO> UpdateRental(string rentalID, RentalUpdateCommand updateRentalData, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var rentalToUpdate = await _dbContext.Rentals.FirstOrDefaultAsync(t => t.RentalID == rentalID, cancellationToken)
            ?? throw new ArgumentException("Аренда не найдена.");

        rentalToUpdate.UserID = updateRentalData.UserID;
        rentalToUpdate.TransportID = updateRentalData.TransportID;
        rentalToUpdate.TimeStart = updateRentalData.TimeStart;
        rentalToUpdate.TimeEnd = updateRentalData.TimeEnd;
        rentalToUpdate.PriceOfUnit = updateRentalData.PriceOfUnit;
        rentalToUpdate.PriceType = updateRentalData.PriceType;
        rentalToUpdate.FinalPrice = updateRentalData.FinalPrice;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ModelToDTO(rentalToUpdate);
    }

    public async Task DeleteRental(string rentalID, CancellationToken cancellationToken = default   )
    {
        cancellationToken.ThrowIfCancellationRequested();

        var rentalToDelete = await _dbContext.Rentals.FirstOrDefaultAsync(t => t.RentalID == rentalID, cancellationToken) 
            ?? throw new ArgumentException("Аренда не найдена.");
        
        _dbContext.Rentals.Remove(rentalToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static RentalDTO ModelToDTO(Rental rental) => new(
        rental.RentalID,
        rental.UserID,
        rental.TransportID,
        rental.TimeStart,
        rental.TimeEnd,
        rental.PriceOfUnit,
        rental.PriceType,
        rental.FinalPrice);

}