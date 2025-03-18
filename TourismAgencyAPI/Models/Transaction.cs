namespace TourismAgencyAPI.Models;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid PackageId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    public User? User { get; set; }
    public TourismPackage? Package { get; set; }
}