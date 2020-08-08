namespace HotelBooking.Domain.Request.HotelServices
{
    public class CreateRoomTypeRequest
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; }
        public int DefaultPrice { get; set; }
        public float Capacity { get; set; }
        public int Quantity { get; set; }
        public bool IsDelete { get; set; }
        public string Description { get; set; }
        public string[] Facilities { get; set; }
        public string[] Images { get; set; }
    }
}
