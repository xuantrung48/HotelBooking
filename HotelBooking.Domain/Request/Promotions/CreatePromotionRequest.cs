using System;

namespace HotelBooking.Domain.Request.Promotions
{
    public class SavePromotionRequest
    {
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float DiscountRates { get; set; }
        public string[] RoomTypeIds { get; set; }
    }
}