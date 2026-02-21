using System.Text.Json.Serialization;

namespace Cabbash.Models
{
    public class HotelDetailsViewModel
    {
        public Hotel? Hotel { get; set; }
        public List<Amenity> Amenities { get; set; } = new();
        public List<RoomType> RoomTypes { get; set; } = new();
        public string? ErrorMessage { get; set; }
        
        // Pass-through search params for booking
        public string? CheckInDate { get; set; }
        public string? CheckOutDate { get; set; }
        public int Adults { get; set; } = 1;
        public int Children { get; set; } = 0;
        public int Rooms { get; set; } = 1;
    }

    public class Amenity
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
    }

    public class RoomType
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("businessPrice")]
        public decimal BusinessPrice { get; set; }

        [JsonPropertyName("minimumDeposit")]
        public decimal MinimumDeposit { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("picturePath")]
        public string? PicturePath { get; set; }
        
        [JsonPropertyName("businessId")]
        public string? BusinessId { get; set; }
    }
}
