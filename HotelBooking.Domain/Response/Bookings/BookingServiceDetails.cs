namespace HotelBooking.Domain.Response.Bookings
{
    public class BookingServiceDetails
    {
        public int BookingServiceDetailsId { get; set; }
        public int BookingId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceQuantity { get; set; }
        public float ServicePrice { get; set; }
    }
}