using SimbirGo.Contracts.Models;

namespace SimbirGo.Contracts.DTO;

public record TransportDTO(
    string TransportID, 
    TransportTypes TransportType, 
    string Model, 
    string RegistrationNumber, 
    bool Availability, 
    double PricePerMinute, 
    double PricePerDay);
