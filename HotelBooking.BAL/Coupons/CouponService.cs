using HotelBooking.BAL.Interface.Coupons;
using HotelBooking.DAL.Interface.Coupons;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Coupons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Coupons
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository couponRepository;

        public CouponService(ICouponRepository couponRepository)
        {
            this.couponRepository = couponRepository;
        }

        public async Task<ActionsResult> Delete(int id)
        {
            return await couponRepository.Delete(id);
        }

        public async Task<IEnumerable<Coupon>> GetAll()
        {
            return await couponRepository.GetAll();
        }

        public async Task<Coupon> GetById(int id)
        {
            return await couponRepository.GetById(id);
        }

        public async Task<ActionsResult> Save(Coupon coupon)
        {
            return await couponRepository.Save(coupon);
        }

        public async Task<CouponSearchResult> Search(string couponCode)
        {
            return await couponRepository.Search(couponCode);
        }
    }
}