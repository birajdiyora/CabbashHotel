using System.Text.Json.Serialization;

namespace Cabbash.Models
{
    public class Location
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("isOpenTill")]
        public bool IsOpenTill { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("telephone")]
        public string? Telephone { get; set; }

        [JsonPropertyName("customerName")]
        public string? CustomerName { get; set; }

        [JsonPropertyName("businessId")]
        public string? BusinessId { get; set; }

        [JsonPropertyName("businessDate")]
        public DateTime BusinessDate { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("isXml")]
        public bool IsXml { get; set; }

        [JsonPropertyName("isError")]
        public bool IsError { get; set; }

        [JsonPropertyName("isFeedback")]
        public bool IsFeedback { get; set; }

        [JsonPropertyName("dateOfChange")]
        public DateTime DateOfChange { get; set; }

        [JsonPropertyName("oldPrice")]
        public string? OldPrice { get; set; }

        [JsonPropertyName("newPrice")]
        public string? NewPrice { get; set; }

        [JsonPropertyName("oldUnitPrice")]
        public string? OldUnitPrice { get; set; }

        [JsonPropertyName("newUnitPrice")]
        public string? NewUnitPrice { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("codeName")]
        public string? CodeName { get; set; }

        [JsonPropertyName("isLocation")]
        public bool IsLocation { get; set; }

        [JsonPropertyName("isAmenity")]
        public bool IsAmenity { get; set; }

        [JsonPropertyName("webVersion")]
        public int WebVersion { get; set; }

        [JsonPropertyName("categoryId")]
        public string? CategoryId { get; set; }

        [JsonPropertyName("terminalId")]
        public string? TerminalId { get; set; }

        [JsonPropertyName("isCategory")]
        public bool IsCategory { get; set; }

        [JsonPropertyName("changedBy")]
        public string? ChangedBy { get; set; }

        [JsonPropertyName("isNewAmenityAndLocation")]
        public bool IsNewAmenityAndLocation { get; set; }

        [JsonPropertyName("amenities")]
        public string? Amenities { get; set; }

        [JsonPropertyName("location")]
        public string? LocationName { get; set; }
    }
}