using System;

namespace HotelBooking.Domain.Response.Bookings
{
    public class BookingRoomDetails
    {
        public int BookingId { get; set; }
        public int RoomTypeId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public float RoomPrice { get; set; }
    }
}
