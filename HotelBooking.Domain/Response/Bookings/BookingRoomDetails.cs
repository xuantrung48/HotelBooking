using System;

namespace HotelBooking.Domain.Response.Bookings
{
    public class BookingRoomDetails
    {
        public int BookingRoomDetailsId { get; set; }
        public int BookingId { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomQuantity { get; set; }
        public DateTime Date { get; set; }
        public float RoomPrice { get; set; }
    }
}