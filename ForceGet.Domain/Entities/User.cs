namespace ForceGet.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
    public virtual ICollection<CurrencyConversion> CurrencyConversions { get; set; } = new List<CurrencyConversion>();
}
