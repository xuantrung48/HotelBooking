using HotelBooking.Domain.Response.Coupons;
using System;
using System.Collections.Generic;

namespace HotelBooking.Domain.Response.Bookings
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public int NumberofAdults { get; set; }
        public int NumberofChildren { get; set; }
        public float ServiceAmount { get; set; }
        public float RoomAmount { get; set; }
        public int? CouponId { get; set; }
        public bool IsCanceled { get; set; }
        public Customer BookingCustomer { get; set; }
        public Coupon BookingCoupon { get; set; }
        public List<BookingRoomDetails> bookingRoomDetails { get; set; }
        public List<BookingServiceDetails> bookingServiceDetails { get; set; }
        //public List<DateTime> listDate { get; set; }
    }
}