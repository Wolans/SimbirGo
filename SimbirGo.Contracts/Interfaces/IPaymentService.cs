using SimbirGo.Contracts.Commands.SimbirGoCommands;
using SimbirGo.Contracts.DTO;

namespace SimbirGo.Contracts.Interfaces;

public interface IPaymentService
{
    Task<PaymentDTO> CreatePayment(string notificationID, PaymentCreateCommand notification, CancellationToken cancellationToken = default);
    Task<PaymentDTO> GetPaymentById(string notificationID, CancellationToken cancellationToken = default);
    Task<ICollection<PaymentDTO>> GetAllPayments(CancellationToken cancellationToken = default);
    Task<PaymentDTO> UpdatePayment(string notificationID, PaymentUpdateCommand notification, CancellationToken cancellationToken = default);
    Task DeletePayment(string notificationID, CancellationToken cancellationToken = default);
}