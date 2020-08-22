using HotelBooking.Domain.Response.HotelServices;

namespace HotelBooking.Domain.Request.Booking
{
    public class RoomTypeBookingRequest
    {
        public RoomType RoomType { get; set; }
        public int Quantity { get; set; }
    }
}