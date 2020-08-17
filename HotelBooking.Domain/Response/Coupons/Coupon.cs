using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
