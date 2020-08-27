using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Promotions;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelBooking.WEB.Controllers
{
    public class PromotionApplyController : Controller
    {
        public JsonResult GetByRoomTypeId(int id)
        {
            List<PromotionApply> result = ApiHelper<List<PromotionApply>>.HttpGetAsync($"{Helper.ApiUrl}api/promotionapply/getbyroomtypeid/{id}");
            return Json(new { result });
        }

        public JsonResult GetByPromotionId(int id)
        {
            List<PromotionApply> result = ApiHelper<List<PromotionApply>>.HttpGetAsync($"{Helper.ApiUrl}api/promotionapply/getbypromotionid/{id}");
            return Json(new { result });
        }

        public JsonResult DeleteByPromotionId(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/promotionapply/deletebypromotionid/{id}", "DELETE");
            return Json(new { result });
        }
    }
}