using HotelBooking.Domain.Response.HotelServices;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Web.Ultilities;
using System.Collections.Generic;
using ActionResult = HotelBooking.Domain.Response.ActionResult;

namespace HotelBooking.WEB.Controllers
{
    public class RoomTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetAll()
        {
            List<RoomType> result = ApiHelper<List<RoomType>>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypes/getall");
            return Json(new { result });
        }

        public JsonResult Get(int id)
        {
            RoomType result = ApiHelper<RoomType>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypes/getbyid/{id}");
            return Json(new { result });
        }

        public JsonResult Delete(int id)
        {
            ActionResult result = ApiHelper<ActionResult>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypes/delete/{id}", "DELETE");
            return Json(new { result });
        }
        public JsonResult Save([FromBody] RoomType model)
        {
            ActionResult result;
            result = ApiHelper<ActionResult>.HttpPostAsync($"{Helper.ApiUrl}api/roomtypes/save", model);
            return Json(new { result });
        }
    }
}
