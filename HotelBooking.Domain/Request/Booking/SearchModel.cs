using System;

namespace HotelBooking.Domain.Request.Booking
{
    public class SearchModel
    {
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public int NumberofAdults { get; set; }
        public int NumberofChildren { get; set; }
    }
}