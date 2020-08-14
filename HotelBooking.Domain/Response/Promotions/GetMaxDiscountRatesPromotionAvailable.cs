using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Domain.Response.Promotions
{
    public class GetMaxDiscountRatesPromotionAvailable
    {
        public int RoomTypeId { get; set; }
        public float DiscountRates { get; set; }
    }
}
