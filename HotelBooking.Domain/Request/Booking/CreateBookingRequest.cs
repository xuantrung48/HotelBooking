using System;
using System.Collections.Generic;

namespace HotelBooking.Domain.Request.Booking
{
    public class CreateBookingRequest
    {
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public int NumberofAdults { get; set; }
        public int NumberofChildren { get; set; }
        public IEnumerable<RoomTypeBookingRequest> RoomTypeBookingRequests { get; set; }
    }
}