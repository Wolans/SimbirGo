using SimbirGo.Contracts.Models;

namespace SimbirGo.Contracts.Commands.SimbirGoCommands;

public record PaymentUpdateCommand(
    string UserID,
    string RentalID,
    double Amount,
    DateTime PaymentDate,
    string Status);