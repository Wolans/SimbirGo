using Microsoft.EntityFrameworkCore;

using SimbirGo.Bll.DbConfiguration;
using SimbirGo.Contracts.Commands.SimbirGoCommands;
using SimbirGo.Contracts.DTO;
using SimbirGo.Contracts.Interfaces;
using SimbirGo.Contracts.Models;

namespace SimbirGo.Bll;

public class TransportService : ITransportService
{
    private readonly SimbirGoDbContext _dbContext;

    public TransportService(SimbirGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TransportDTO> CreateTransport(string transportID, TransportCreateCommand transport, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Transport newTransport = new(transportID,
            transport.TransportType,
            transport.Model,
            transport.RegistrationNumber,
            transport.Availability,
            transport.PricePerMinute,
            transport.PricePerDay);

        await _dbContext.Transports.AddAsync(newTransport, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ModelToDTO(newTransport);
    }

    public async Task<TransportDTO> GetTransportById(string transportID, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existingTransport = await _dbContext.Transports.AsNoTracking().FirstOrDefaultAsync(t => t.TransportID == transportID, cancellationToken)
            ?? throw new ArgumentException("Транспорт не найден.");

        return ModelToDTO(existingTransport);
    }

    public async Task<ICollection<TransportDTO>> GetAllTransports(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _dbContext.Transports.AsNoTracking()
        .Select(transport => ModelToDTO(transport))
        .ToListAsync(cancellationToken);
    }

    public async Task<TransportDTO> UpdateTransport(string transportID, TransportUpdateCommand updateTransportData, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var transportToUpdate = await _dbContext.Transports.FirstOrDefaultAsync(t => t.TransportID == transportID, cancellationToken)
            ?? throw new ArgumentException("Транспорт не найден.");

        transportToUpdate.TransportType = updateTransportData.TransportType;
        transportToUpdate.Model = updateTransportData.Model;
        transportToUpdate.RegistrationNumber = updateTransportData.RegistrationNumber;
        transportToUpdate.Availability = updateTransportData.Availability;
        transportToUpdate.PricePerMinute = updateTransportData.PricePerMinute;
        transportToUpdate.PricePerDay = updateTransportData.PricePerDay;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ModelToDTO(transportToUpdate);
    }

    public async Task DeleteTransport(string transportID, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var transportToDelete = await _dbContext.Transports.FirstOrDefaultAsync(t => t.TransportID == transportID, cancellationToken) 
            ?? throw new ArgumentException("Транспорт не найден.");
        
        _dbContext.Transports.Remove(transportToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static TransportDTO ModelToDTO(Transport transport) => new(
        transport.TransportID,
        transport.TransportType,
        transport.Model,
        transport.RegistrationNumber,
        transport.Availability,
        transport.PricePerMinute,
        transport.PricePerDay);

}