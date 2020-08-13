using HotelBooking.Domain.Response.Promotions;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Web.Ultilities;
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
    }
}
