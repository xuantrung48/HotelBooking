namespace HotelBooking.Domain.Response.HotelServices
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public bool IsOccupied { get; set; }
    }
}