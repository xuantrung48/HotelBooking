using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Coupons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.Coupons
{
    public interface ICouponRepository
    {
        Task<IEnumerable<Coupon>> GetAll();
        Task<ActionsResult> Save(Coupon coupon);
        Task<Coupon> GetById(int id);
        Task<ActionsResult> Delete(int id);
    }
}
