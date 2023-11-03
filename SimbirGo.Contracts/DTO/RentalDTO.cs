using SimbirGo.Contracts.Models;

namespace SimbirGo.Contracts.DTO;

public record RentalDTO(
    string RentalID, 
    string UserID, 
    string TransportID, 
    DateTime TimeStart, 
    DateTime? TimeEnd, 
    double PriceOfUnit, 
    PriceTypes PriceType, 
    double? FinalPrice);
