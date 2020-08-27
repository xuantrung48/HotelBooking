using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Domain.Response.Coupons
{
    public class CouponSearchResult
    {
        public int CouponId { get; set; }
        public float Reduction { get; set; }
        public string Message { get; set; }
    }
}
