namespace SimbirGo.Contracts.Models;

public enum TransportTypes
{
    Automobile = 0,
    Motorcycle = 1,
    Bicycle = 2
}

public class Transport
{
    public string TransportID { get; set; }
    public TransportTypes TransportType { get; set; }
    public string Model { get; set; }
    public string RegistrationNumber { get; set; }
    public bool Availability { get; set; }
    public double PricePerMinute { get; set; }
    public double PricePerDay { get; set; }
    public ICollection<Rental> Rentals { get; }

    public Transport (string transportID, TransportTypes transportType, string model, string registrationNumber, bool availability, double pricePerMinute, double pricePerDay)
    {
        TransportID = transportID;
        TransportType = transportType;
        Model = model;
        RegistrationNumber = registrationNumber;
        Availability = availability;
        PricePerMinute = pricePerMinute;
        PricePerDay = pricePerDay;
        Rentals = new HashSet<Rental> ();
    }
}
