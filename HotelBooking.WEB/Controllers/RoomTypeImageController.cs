using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelBooking.WEB.Controllers
{
    public class RoomTypeImageController : Controller
    {
        public JsonResult GetByRoomTypeId(int id)
        {
            List<RoomTypeImage> result = ApiHelper<List<RoomTypeImage>>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypeimages/getbyroomtypeid/{id}");
            return Json(new { result });
        }

        public JsonResult Delete(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypeimages/delete/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult Save([FromBody] RoomTypeImage model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/roomtypeimages/save", model);
            return Json(new { result });
        }
    }
}