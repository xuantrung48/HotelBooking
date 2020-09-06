using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Coupons;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelBooking.WEB.Controllers
{
    public class CouponsManagerController : Controller
    {
        public IActionResult Index()
        {
            Coupon view = new Coupon();
            return View(view);
        }

        public JsonResult Get(int id)
        {
            Coupon result = ApiHelper<Coupon>.HttpGetAsync($"{Helper.ApiUrl}api/coupon/getbyid/{id}");
            return Json(new { result });
        }

        public JsonResult GetAll()
        {
            List<Coupon> result = ApiHelper<List<Coupon>>.HttpGetAsync($"{Helper.ApiUrl}api/coupon/getall");
            return Json(new { result });
        }

        public JsonResult Delete(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/coupon/delete/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult Save([FromBody] Coupon model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/coupon/save", model);
            return Json(new { result });
        }
        public JsonResult Search(string id)
        {
            CouponSearchResult result = ApiHelper<CouponSearchResult>.HttpGetAsync($"{Helper.ApiUrl}api/coupon/search/{id}");
            return Json(new { result });
        }
    }
}