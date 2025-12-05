namespace ForceGet.Domain.Entities
{
    public class CurrencyConversion
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FromCurrency { get; set; } = string.Empty;
        public string ToCurrency { get; set; } = string.Empty;
        public decimal OriginalAmount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public DateTime ConvertedAt { get; set; } = DateTime.UtcNow;
        public User? User { get; set; }
    }
}
