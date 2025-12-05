using System.Text.Json.Serialization;

namespace ForceGet.Application.DTOs
{
    public class CurrencyExchangeDto
    {
        [JsonPropertyName("old_amount")]
        public decimal OldAmount { get; set; }
        [JsonPropertyName("old_currency")]
        public string OldCurrency { get; set; } = string.Empty;
        [JsonPropertyName("new_currency")]
        public string NewCurrency { get; set; } = string.Empty;
        [JsonPropertyName("new_amount")]
        public decimal NewAmount { get; set; }
    }
}
