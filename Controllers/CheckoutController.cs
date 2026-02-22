using Microsoft.AspNetCore.Mvc;
using Cabbash.Models;
using System.Text.Json;

namespace Cabbash.Controllers;

public class CheckoutController : Controller
{
    private readonly HttpClient _httpClient;

    public CheckoutController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<IActionResult> Index(
        string? hotelId,
        string? roomTypeId,
        string? checkInDate,
        string? checkOutDate,
        int adults = 1,
        int children = 0,
        int rooms = 1)
    {
        var model = new BookingViewModel();

        // If we have parameters from the booking flow, fetch from API
        if (!string.IsNullOrEmpty(hotelId) && !string.IsNullOrEmpty(roomTypeId))
        {
            try
            {
                // Fetch hotel details from API
                var apiUrl = $"https://cabbashhotelapi.azurewebsites.net/GetHotel/?id={hotelId}";
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var hotel = JsonSerializer.Deserialize<Hotel>(jsonContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (hotel != null)
                    {
                        // Find the selected room type
                        var selectedRoomType = hotel.RoomTypes?.FirstOrDefault(rt => rt.Id == roomTypeId);

                        // Populate model with hotel data
                        model.HotelId = hotelId;
                        model.HotelName = hotel.BusinessName ?? "Hotel";
                        model.Location = hotel.FullAddress ?? hotel.Address ?? "Location not available";
                        model.Rating = 5.0; // You might want to get this from the API if available
                        model.HotelImage = hotel.HotelImages?.FirstOrDefault() ?? hotel.ImageUrl ?? "/img/innerpages/hotel-img3.jpg";
                        
                        // Populate room data
                        if (selectedRoomType != null)
                        {
                            model.RoomTypeId = roomTypeId;
                            model.RoomType = selectedRoomType.Name ?? "Standard Room";
                            model.RoomImage = selectedRoomType.PicturePath ?? model.HotelImage;
                            model.RoomDescription = selectedRoomType.Description ?? "";
                            model.OriginalPrice = selectedRoomType.Price;
                            model.Discount = selectedRoomType.Price - selectedRoomType.BusinessPrice;
                            model.TotalPrice = selectedRoomType.BusinessPrice * rooms;
                        }

                        // Parse dates
                        if (DateTime.TryParse(checkInDate, out var checkIn))
                            model.CheckInDate = checkIn;
                        else
                            model.CheckInDate = DateTime.Now.AddDays(1);

                        if (DateTime.TryParse(checkOutDate, out var checkOut))
                            model.CheckOutDate = checkOut;
                        else
                            model.CheckOutDate = DateTime.Now.AddDays(2);

                        model.NumberOfNights = (model.CheckOutDate - model.CheckInDate).Days;
                        model.NumberOfRooms = rooms;
                        model.NumberOfAdults = adults;
                        model.NumberOfChildren = children;

                        // Calculate total price based on nights
                        if (selectedRoomType != null)
                        {
                            model.TotalPrice = selectedRoomType.BusinessPrice * rooms * model.NumberOfNights;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error and use default values
                Console.WriteLine($"Error fetching hotel details: {ex.Message}");
            }
        }

        // If model is still empty, use default values
        if (string.IsNullOrEmpty(model.HotelName))
        {
            model.HotelName = "Bulgari Hotels & Resorts";
            model.Location = "Indonesia";
            model.Rating = 5.0;
            model.CheckInDate = DateTime.Now.AddDays(1);
            model.CheckOutDate = DateTime.Now.AddDays(2);
            model.NumberOfNights = 1;
            model.NumberOfRooms = 2;
            model.NumberOfAdults = 2;
            model.RoomType = "Deluxe Double Room";
            model.OriginalPrice = 4998;
            model.Discount = 2499;
            model.TotalPrice = 2499;
        }
        
        return View(model);
    }

    [HttpPost]
    public IActionResult ProcessBooking(BookingViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        // Generate a booking ID
        model.BookingId = $"HBK-{new Random().Next(100000, 999999)}";
        model.PaymentStatus = "Paid";
        
        return RedirectToAction("Confirmation", model);
    }

    public IActionResult Confirmation(BookingViewModel model)
    {
        // if (string.IsNullOrEmpty(model.BookingId))
        // {
        //     return RedirectToAction("Index");
        // }
        
        return View(model);
    }
}
