using SimbirGo.Contracts.Models;

namespace SimbirGo.Contracts.Commands.SimbirGoCommands;

public record TransportUpdateCommand(
    string TransportID,
    TransportTypes TransportType,
    string Model,
    string RegistrationNumber,
    bool Availability,
    double PricePerMinute,
    double PricePerDay);