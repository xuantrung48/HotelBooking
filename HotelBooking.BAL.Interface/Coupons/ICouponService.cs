﻿using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Coupon;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.Coupons
{
    public interface ICouponService
    {
        Task<IEnumerable<Coupon>> GetAll();
        Task<Coupon> GetById(int id);
        Task<ActionsResult> Save(Coupon coupon);
    }
}