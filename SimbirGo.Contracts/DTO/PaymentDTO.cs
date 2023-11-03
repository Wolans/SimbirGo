using SimbirGo.Contracts.Models;

namespace SimbirGo.Contracts.DTO;

public record PaymentDTO(
    string PaymentID, 
    string UserID, 
    string RentalID, 
    double Amount, 
    DateTime PaymentDate, 
    string Status);