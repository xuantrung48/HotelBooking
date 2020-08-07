﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Domain.Response.Coupon
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public int Remain { get; set; }
        public float Reduction { get; set; }
        public DateTime EndDate { get; set; }
    }
}
