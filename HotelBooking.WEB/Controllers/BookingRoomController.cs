using HotelBooking.Domain.Response.HotelServices;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Web.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.WEB.Controllers
{
    public class BookingRoomController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //public JsonResult GetRoomType(int id, int minRemain)
        //{
        //    var listRoomType = ApiHelper<List<RoomType>>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypes/getall");
        //    var result = (from 
        //    return Json(new { result });
        //}
    }
}
