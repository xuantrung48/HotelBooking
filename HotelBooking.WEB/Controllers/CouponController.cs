using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Coupons;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Web.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.WEB.Controllers
{
    public class CouponController: Controller
    {
        public IActionResult Index()
        {
            return View();
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
    }
}