using HotelBooking.Domain.Request.Booking;
using HotelBooking.Domain.Request.Promotions;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Promotions;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HotelBooking.WEB.Controllers
{
    public class PromotionsManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int id)
        {
            Promotion result = ApiHelper<Promotion>.HttpGetAsync($"{Helper.ApiUrl}api/promotions/getbyid/{id}");
            return Json(new { result });
        }

        public JsonResult GetAll()
        {
            List<Promotion> result = ApiHelper<List<Promotion>>.HttpGetAsync($"{Helper.ApiUrl}api/promotions/getall");
            return Json(new { result });
        }

        public JsonResult Delete(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/promotions/delete/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult Save([FromBody] SavePromotionRequest model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/promotions/save", model);
            return Json(new { result });
        }

        public JsonResult GetAvailable()
        {
            List<GetMaxDiscountRatesPromotionAvailable> result = ApiHelper<List<GetMaxDiscountRatesPromotionAvailable>>.HttpGetAsync($"{Helper.ApiUrl}api/promotions/getavailable");
            return Json(new { result });
        }
        public JsonResult GetAvailableForDate([FromBody] DateTime date)
        {
            List<GetMaxDiscountRatesPromotionAvailable> result = ApiHelper<List<GetMaxDiscountRatesPromotionAvailable>>.HttpPostAsync($"{Helper.ApiUrl}api/promotions/getavailablefordate", date);
            return Json(new { result });
        }
        public JsonResult GetAvailableForDateAndRoomTypeId([FromBody] GetAvailablePromotionForDateAndRoomIdRequest request)
        {
            var result = ApiHelper<GetAvailablePromotionForDateAndRoomIdResponse>.HttpPostAsync($"{Helper.ApiUrl}api/promotions/getavailablefordateandroomtypeid", request);
            return Json(new { result });
        }
    }
}