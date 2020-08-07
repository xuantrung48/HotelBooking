using System;

namespace HotelBooking.Domain.Response.Bookings
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreateDate { get; set; }
        public float ServiceAmount { get; set; }
        public float RoomAmount { get; set; }
        public bool IsCanceled { get; set; }
    }
}