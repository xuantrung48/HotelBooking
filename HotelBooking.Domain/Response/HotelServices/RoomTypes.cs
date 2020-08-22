using HotelBooking.Domain.Response.Facilities;
using System.Collections.Generic;

namespace HotelBooking.Domain.Response.HotelServices
{
    public class RoomTypes
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; }
        public int DefaultPrice { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int MaxAdult { get; set; }
        public int MaxChildren { get; set; }
        public int MaxPeople { get; set; }
        public string Image { get; set; }
        public IEnumerable<Facility> Facilities { get; set; }
    }
}