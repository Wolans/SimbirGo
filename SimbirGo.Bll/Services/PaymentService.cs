using Microsoft.EntityFrameworkCore;

using SimbirGo.Bll.DbConfiguration;
using SimbirGo.Contracts.Commands.SimbirGoCommands;
using SimbirGo.Contracts.DTO;
using SimbirGo.Contracts.Interfaces;
using SimbirGo.Contracts.Models;

namespace SimbirGo.Bll;

public class PaymentService : IPaymentService
{
    private readonly SimbirGoDbContext _dbContext;

    public PaymentService(SimbirGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaymentDTO> CreatePayment(string paymentID, PaymentCreateCommand payment, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Payment newPayment = new(paymentID,
            payment.UserID,
            payment.RentalID,
            payment.Amount,
            payment.PaymentDate,
            payment.Status);

        await _dbContext.Payments.AddAsync(newPayment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ModelToDTO(newPayment);
    }

    public async Task<PaymentDTO> GetPaymentById(string paymentID, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existingPayment = await _dbContext.Payments.AsNoTracking().FirstOrDefaultAsync(t => t.PaymentID == paymentID, cancellationToken)
            ?? throw new ArgumentException("Оплата не найдена.");

        return ModelToDTO(existingPayment);
    }

    public async Task<ICollection<PaymentDTO>> GetAllPayments(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _dbContext.Payments.AsNoTracking()
        .Select(payment => ModelToDTO(payment))
        .ToListAsync(cancellationToken);
    }
    public async Task<PaymentDTO> UpdatePayment(string paymentID, PaymentUpdateCommand updatePaymentData, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var paymentToUpdate = await _dbContext.Payments.FirstOrDefaultAsync(t => t.PaymentID == paymentID, cancellationToken)
            ?? throw new ArgumentException("Оплата не найдена.");

        paymentToUpdate.UserID = updatePaymentData.UserID;
        paymentToUpdate.RentalID = updatePaymentData.RentalID;
        paymentToUpdate.Amount = updatePaymentData.Amount;
        paymentToUpdate.PaymentDate = updatePaymentData.PaymentDate;
        paymentToUpdate.Status = updatePaymentData.Status;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ModelToDTO(paymentToUpdate);
    }

    public async Task DeletePayment(string paymentID, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var paymentToDelete = await _dbContext.Payments.FirstOrDefaultAsync(t => t.PaymentID == paymentID, cancellationToken) 
            ?? throw new ArgumentException("Оплата не найдена.");
        
        _dbContext.Payments.Remove(paymentToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static PaymentDTO ModelToDTO(Payment payment) => new(
        payment.PaymentID,
        payment.UserID,
        payment.RentalID,
        payment.Amount,
        payment.PaymentDate,
        payment.Status);

}