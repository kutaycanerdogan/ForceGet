using ForceGet.Domain.Enums;

namespace ForceGet.Domain.Entities;

public class Quote
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public ShippingMode Mode { get; set; }
    public MovementType MovementType { get; set; }
    public Incoterms Incoterms { get; set; }
    public PackageType PackageType { get; set; }
    public CurrencyType FromCurrency { get; set; }
    public decimal OriginalAmount { get; set; }
    public CurrencyType ToCurrency { get; set; }
    public decimal ConvertedAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public User? User { get; set; }
}
