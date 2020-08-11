using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Coupons;
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
        Task<ActionsResult> Delete(int id);
    }
}
