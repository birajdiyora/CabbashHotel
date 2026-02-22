using System.Text.Json.Serialization;

namespace Cabbash.Models
{
    public class Hotel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        
        [JsonPropertyName("businessName")]
        public string BusinessName { get; set; } = string.Empty;
        
        [JsonPropertyName("businessLocation")]
        public string Address { get; set; } = string.Empty;
        
        [JsonPropertyName("fullAddress")]
        public string? FullAddress { get; set; }
        
        [JsonPropertyName("businessTown")]
        public string City { get; set; } = string.Empty;
        
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;
        
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        
        [JsonPropertyName("businessLogoPath")]
        public string ImageUrl { get; set; } = string.Empty;
        
        [JsonPropertyName("businessHeaderLogoPath")]
        public string HeaderLogoPath { get; set; } = string.Empty;
        
        [JsonPropertyName("homePagePics")]
        public List<string> HotelImages { get; set; } = new List<string>();
        
        [JsonPropertyName("videoPath")]
        public string? VideoPath { get; set; }
        
        [JsonPropertyName("roomTypes")]
        public List<RoomType>? RoomTypes { get; set; }
        
        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }
        
        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }
        
        // Computed property for display
        public string Location => FullAddress ?? Address ??
                                 (!string.IsNullOrEmpty(City) ? City : "") +
                                 (!string.IsNullOrEmpty(Country) ? (string.IsNullOrEmpty(City) ? Country : $", {Country}") : "") ??
                                 "Location not specified";
        
        // Default values for display (not from API)
        public decimal PricePerNight { get; set; } = 140;
        public double Rating { get; set; } = 4.5;
        public List<string> Amenities { get; set; } = new List<string> { "Free Wi-Fi", "Air Conditioning", "Swimming Pool", "Bars & Lounges" };
        public bool FreeCancellation { get; set; } = true;
    }
}