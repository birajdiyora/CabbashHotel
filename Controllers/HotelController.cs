using Microsoft.AspNetCore.Mvc;
using Cabbash.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Cabbash.Controllers
{
    public class HotelController : Controller
    {
        private readonly ILogger<HotelController> _logger;
        private readonly HttpClient _httpClient;

        public HotelController(ILogger<HotelController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(string? locationId, string? checkInDate, string? checkOutDate, int adults = 1, int children = 0, int rooms = 1)
        {
            var viewModel = new HotelSearchViewModel
            {
                LocationId = locationId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                Adults = adults,
                Children = children,
                Rooms = rooms,
                HasFilters = !string.IsNullOrEmpty(locationId) || !string.IsNullOrEmpty(checkInDate)
            };

            try
            {
                // Load locations
                viewModel.Locations = await GetLocationsAsync();
                
                // Set location name from locations list if locationId is provided
                if (!string.IsNullOrEmpty(locationId))
                {
                    var selectedLocation = viewModel.Locations.FirstOrDefault(l => l.Id == locationId);
                    viewModel.LocationName = selectedLocation?.Name;
                }

                // Load hotels
                viewModel.Hotels = await GetHotelsAsync();
                
                // Filter hotels by location if specified
                if (!string.IsNullOrEmpty(locationId))
                {
                    var selectedLocation = viewModel.Locations.FirstOrDefault(l => l.Id == locationId);
                    if (selectedLocation != null)
                    {
                        // Filter hotels by country or location name match
                        viewModel.Hotels = viewModel.Hotels.Where(h => 
                            (!string.IsNullOrEmpty(h.Country) && h.Country.Contains(selectedLocation.Name ?? "", StringComparison.OrdinalIgnoreCase)) ||
                            (!string.IsNullOrEmpty(h.Address) && h.Address.Contains(selectedLocation.Name ?? "", StringComparison.OrdinalIgnoreCase)) ||
                            (!string.IsNullOrEmpty(h.City) && h.City == selectedLocation.Id)
                        ).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading hotel page data");
                viewModel.ErrorMessage = "Unable to load hotel data. Please try again.";
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Details(string id, string? checkInDate, string? checkOutDate, int adults = 1, int children = 0, int rooms = 1)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");

            var viewModel = new HotelDetailsViewModel
            {
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                Adults = adults,
                Children = children,
                Rooms = rooms
            };

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
            };

            try
            {
                var hotelTask      = _httpClient.GetAsync($"https://cabbashhotelapi.azurewebsites.net/GetHotel/?id={id}");
                var amenitiesTask  = _httpClient.GetAsync($"https://cabbashhotelapi.azurewebsites.net/GetAllAmenitiesByHotelId/?id={id}");
                var roomTypesTask  = _httpClient.GetAsync($"https://cabbashhotelapi.azurewebsites.net/GetHotelRoomTypes/?id={id}");

                await Task.WhenAll(hotelTask, amenitiesTask, roomTypesTask);

                if (hotelTask.Result.IsSuccessStatusCode)
                {
                    var json = await hotelTask.Result.Content.ReadAsStringAsync();
                    viewModel.Hotel = JsonSerializer.Deserialize<Hotel>(json, options);
                    
                    // Log the coordinates for debugging
                    _logger.LogInformation($"Hotel Coordinates - Latitude: {viewModel.Hotel?.Latitude}, Longitude: {viewModel.Hotel?.Longitude}");
                }

                if (amenitiesTask.Result.IsSuccessStatusCode)
                {
                    var json = await amenitiesTask.Result.Content.ReadAsStringAsync();
                    viewModel.Amenities = JsonSerializer.Deserialize<List<Amenity>>(json, options) ?? new();
                }

                if (roomTypesTask.Result.IsSuccessStatusCode)
                {
                    var json = await roomTypesTask.Result.Content.ReadAsStringAsync();
                    var all = JsonSerializer.Deserialize<List<RoomType>>(json, options) ?? new();
                    viewModel.RoomTypes = all.Where(r => r.IsActive).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading hotel details for id {Id}", id);
                viewModel.ErrorMessage = "Unable to load hotel details. Please try again.";
            }

            return View(viewModel);
        }

        private async Task<List<Location>> GetLocationsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://cabbashhotelapi.azurewebsites.net/GetAllLocations");
                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var locations = JsonSerializer.Deserialize<List<Location>>(jsonContent, new JsonSerializerOptions 
                    { 
                        PropertyNameCaseInsensitive = true 
                    });
                    return locations ?? new List<Location>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching locations from API");
            }
            
            return new List<Location>();
        }

        private async Task<List<Hotel>> GetHotelsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://cabbashhotelapi.azurewebsites.net/gethotels");
                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions 
                    { 
                        PropertyNameCaseInsensitive = true,
                        NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
                    };
                    var hotels = JsonSerializer.Deserialize<List<Hotel>>(jsonContent, options);
                    return hotels ?? new List<Hotel>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching hotels from API");
            }
            
            return new List<Hotel>();
        }

        [HttpGet]
        public async Task<IActionResult> CheckRoomAvailability(string id, string roomId, string startDate, string endDate)
        {
            try
            {
                var apiUrl = $"https://cabbashhotelapi.azurewebsites.net/GetHotelAvailabilityByRoomId/?id={id}&roomId={roomId}&startDate={startDate}&endDate={endDate}";
                
                var response = await _httpClient.GetAsync(apiUrl);
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    return Content(jsonContent, "application/json");
                }
                else
                {
                    _logger.LogError($"API returned status code: {response.StatusCode}");
                    return StatusCode((int)response.StatusCode, new { error = "Failed to check availability" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking room availability");
                return StatusCode(500, new { error = "An error occurred while checking availability" });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}