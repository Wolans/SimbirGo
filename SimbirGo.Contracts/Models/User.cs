namespace SimbirGo.Contracts.Models;

public class User
{
    public string UserID { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<Rental> Rentals { get; }
    public ICollection<Payment> Payments { get; }

    public User (string userID, string userName, string passwordHash, string email, string role, DateTime createdAt, DateTime updatedAt)
    {
        UserID = userID;
        UserName = userName;
        PasswordHash = passwordHash;
        Email = email;
        Role = role;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Rentals = new HashSet<Rental> ();
        Payments = new HashSet<Payment> ();
    }
}
