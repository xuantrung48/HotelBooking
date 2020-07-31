using System;

namespace HotelBooking.Domain.Response
{
    public class Promotion
    {
        public int PromotionId {get;set;}
        public int RoomTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float DiscountRates {get;set;}
    }
}
