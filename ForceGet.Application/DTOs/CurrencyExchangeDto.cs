
namespace ForceGet.Application.DTOs
{
    public class CurrencyExchangeDto
    {
        public decimal OldAmount { get; set; }
        public string OldCurrency { get; set; } = string.Empty;
        public string NewCurrency { get; set; } = string.Empty;
        public decimal NewAmount { get; set; }
    }
}
