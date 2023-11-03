using SimbirGo.Contracts.Models;

namespace SimbirGo.Contracts.Commands.SimbirGoCommands;

public record PaymentCreateCommand(
    string UserID,
    string RentalID,
    double Amount,
    DateTime PaymentDate,
    string Status);