using Microsoft.AspNetCore.Mvc;
using Cabbash.Models;

namespace Cabbash.Controllers;

public class CheckoutController : Controller
{
    public IActionResult Index()
    {
        var model = new BookingViewModel
        {
            HotelName = "Bulgari Hotels & Resorts",
            Location = "Indonesia",
            Rating = 5.0,
            CheckInDate = DateTime.Now.AddDays(1),
            CheckOutDate = DateTime.Now.AddDays(2),
            NumberOfNights = 1,
            NumberOfRooms = 2,
            NumberOfAdults = 2,
            RoomType = "Deluxe Double Room",
            OriginalPrice = 4998,
            Discount = 2499,
            TotalPrice = 2499
        };
        
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
