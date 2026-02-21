namespace Cabbash.Models
{
    public class HomePageViewModel
    {
        public List<Hotel> Hotels { get; set; } = new List<Hotel>();
        public List<Location> Locations { get; set; } = new List<Location>();
    }
}