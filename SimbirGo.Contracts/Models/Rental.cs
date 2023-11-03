namespace SimbirGo.Contracts.Models;

public enum PriceTypes
{
    Minutes = 0,
    Days = 1
}

public class Rental
{
    public string RentalID { get; set; }
    public string UserID { get; set; }
    public string TransportID { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime? TimeEnd { get; set; }
    public double PriceOfUnit { get; set; }
    public PriceTypes PriceType { get; set; }
    public double? FinalPrice { get; set; }
    public User? User { get; set; }
    public Transport? Transport { get; set; }
    public Payment Payment { get; set; } = null!;

    public Rental (string rentalID, string userID, string transportID, DateTime timeStart, DateTime? timeEnd, double priceOfUnit, PriceTypes priceType, double? finalPrice)
    {
        RentalID = rentalID;
        UserID = userID;
        TransportID = transportID;
        TimeStart = timeStart;
        TimeEnd = timeEnd;
        PriceOfUnit = priceOfUnit;
        PriceType = priceType;
        FinalPrice = finalPrice;
    }
}
