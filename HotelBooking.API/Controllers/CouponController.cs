using HotelBooking.BAL.Interface.Coupons;
using HotelBooking.Domain.Request.Coupon;
using HotelBooking.Domain.Response.Coupons;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionsResult = HotelBooking.Domain.Response.ActionsResult;

namespace HotelBooking.API.Controllers
{
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService couponService;

        public CouponController(ICouponService couponService)
        {
            this.couponService = couponService;
        }

        [HttpGet]
        [Route("api/coupon/getbyid/{id}")]
        public async Task<Coupon> GetById(int id)
        {
            return await couponService.GetById(id);
        }

        [HttpGet]
        [Route("api/coupon/getall")]
        public async Task<IEnumerable<Coupon>> GetAll()
        {
            return await couponService.GetAll();
        }

        [HttpPost]
        [Route("api/coupon/save")]
        public async Task<ActionsResult> Save(Coupon coupon)
        {
            return await couponService.Save(coupon);
        }

        [HttpDelete]
        [Route("api/coupon/delete/{id}")]
        public async Task<ActionsResult> Delete(int id)
        {
            return await couponService.Delete(id);
        }

        [HttpPost]
        [Route("api/coupon/search")]
        public async Task<CouponSearchResult> Search(CouponSearchRequest couponSearchRequest)
        {
            return await couponService.Search(couponSearchRequest);
        }
    }
}