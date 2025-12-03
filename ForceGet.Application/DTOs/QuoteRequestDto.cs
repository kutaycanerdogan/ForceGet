using ForceGet.Domain.Enums;

namespace ForceGet.Application.DTOs;

public class QuoteRequestDto
{
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public ShippingMode Mode { get; set; }
    public MovementType MovementType { get; set; }
    public Incoterms Incoterms { get; set; }
    public PackageType PackageType { get; set; }
    public CurrencyType Currency { get; set; }
    public decimal OriginalAmount { get; set; }
}
