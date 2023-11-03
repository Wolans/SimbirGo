using SimbirGo.Contracts.Models;

namespace SimbirGo.Contracts.Commands.SimbirGoCommands;

public record RentalCreateCommand(
    string UserID,
    string TransportID,
    DateTime TimeStart,
    DateTime? TimeEnd,
    double PriceOfUnit,
    PriceTypes PriceType,
    double? FinalPrice);