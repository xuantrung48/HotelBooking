namespace HotelBooking.Domain.Request.HotelServices
{
    public class CreateRoomTypeRequest
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; }
        public int DefaultPrice { get; set; }
        public int MaxAdult { get; set; }
        public int MaxChildren { get; set; }
        public int MaxPeople { get; set; }
        public int Quantity { get; set; }
        public bool IsDelete { get; set; }
        public string Description { get; set; }
        public string[] Facilities { get; set; }
        public string[] Images { get; set; }
    }
}