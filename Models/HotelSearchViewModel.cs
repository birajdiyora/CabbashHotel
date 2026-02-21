using Cabbash.Models;

namespace Cabbash.Models
{
    public class HotelSearchViewModel
    {
        public List<Location> Locations { get; set; } = new List<Location>();
        public List<Hotel> Hotels { get; set; } = new List<Hotel>();
        
        // Filter properties
        public string? LocationId { get; set; }
        public string? LocationName { get; set; }
        public string? CheckInDate { get; set; }
        public string? CheckOutDate { get; set; }
        public int Adults { get; set; } = 1;
        public int Children { get; set; } = 0;
        public int Rooms { get; set; } = 1;
        
        // Search state
        public bool HasFilters { get; set; }
        public string? ErrorMessage { get; set; }
    }
}