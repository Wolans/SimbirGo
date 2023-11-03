namespace SimbirGo.Contracts.Models;

public class Payment
{
    public string PaymentID { get; set; }
    public string UserID { get; set; }
    public string RentalID { get; set; }
    public double Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Status { get; set; }
    public User? User { get; set; }
    public Rental? Rental { get; set; }

    public Payment (string paymentID, string userID, string rentalID, double amount, DateTime paymentDate, string status)
    {
        PaymentID = paymentID;
        UserID = userID;
        RentalID = rentalID;
        Amount = amount;
        PaymentDate = paymentDate;
        Status = status;
    }
}
