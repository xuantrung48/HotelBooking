using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Domain.Response.Coupons
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }

        [Required]
        public int Remain { get; set; }

        public float Reduction { get; set; }
        public DateTime EndDate { get; set; }
    }
}