using System;

namespace HotelBooking.Domain.Response.Promotions
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float DiscountRates { get; set; }
        public bool IsDeleted { get; set; }
    }
}