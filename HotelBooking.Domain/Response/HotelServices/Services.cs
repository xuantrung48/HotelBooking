namespace HotelBooking.Domain.Response.HotelServices
{
    public class Services
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int Price { get; set; }
        public bool IsDelete { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}