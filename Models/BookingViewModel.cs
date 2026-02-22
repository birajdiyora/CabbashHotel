using System.ComponentModel.DataAnnotations;

namespace Cabbash.Models;

public class BookingViewModel
{
    // Hotel Information
    public string HotelId { get; set; } = string.Empty;
    public string HotelName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string HotelImage { get; set; } = string.Empty;
    public List<string>? HotelFeatures { get; set; }

    // Booking Details
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfNights { get; set; }
    public int NumberOfRooms { get; set; }
    public int NumberOfAdults { get; set; }
    public int NumberOfChildren { get; set; }
    
    // Room Information
    public string RoomTypeId { get; set; } = string.Empty;
    public string RoomType { get; set; } = string.Empty;
    public string RoomImage { get; set; } = string.Empty;
    public string RoomDescription { get; set; } = string.Empty;

    // Guest Information
    [Required(ErrorMessage = "Full name is required")]
    public string GuestName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }

    public string? GuestLocation { get; set; }

    [Required(ErrorMessage = "Street address is required")]
    public string StreetAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Postal code is required")]
    public string PostalCode { get; set; } = string.Empty;

    public string? SpecialRequests { get; set; }

    public bool SaveInformation { get; set; }

    // Pricing
    public decimal OriginalPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }

    // Payment
    public string? PromoCode { get; set; }
    public string? PaymentMethod { get; set; }
    public string? PaymentStatus { get; set; }

    // Confirmation
    public string? BookingId { get; set; }
}
