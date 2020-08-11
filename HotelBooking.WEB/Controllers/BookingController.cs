using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using HotelBooking.Domain.Response.Coupons;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Web.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.WEB.Controllers
{
    public class BookingController: Controller
    {
        public IActionResult Index()
        {
            ViewBag.Coupon = GetAllCoupon();
            return View();
        }
        public JsonResult Get(int id)
        {
            Booking result = ApiHelper<Booking>.HttpGetAsync($"{Helper.ApiUrl}api/booking/get/{id}");
            return Json(new { result });
        }

        public JsonResult GetAll()
        {
            List<Booking> result = ApiHelper<List<Booking>>.HttpGetAsync($"{Helper.ApiUrl}api/booking/get");
            return Json(new { result });
        }

        public JsonResult Delete(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/booking/get/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult Save([FromBody] Booking model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/booking/save", model);
            return Json(new { result });
        }
        private List<Coupon> GetAllCoupon()
        {
            return ApiHelper<List<Coupon>>.HttpGetAsync($"{Helper.ApiUrl}api/coupon/getall");
        }
    }
}
