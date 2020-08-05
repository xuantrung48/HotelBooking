using HotelBooking.Domain.Response.HotelServices;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Web.Ultilities;
using System.Collections.Generic;
using ActionsResult = HotelBooking.Domain.Response.ActionsResult;

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
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypes/delete/{id}", "DELETE");
            return Json(new { result });
        }
        public JsonResult Save([FromBody] RoomType model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/roomtypes/save", model);
            return Json(new { result });
        }
    }
}
