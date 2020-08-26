using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelBooking.WEB.Controllers
{
    public class RoomTypesManagerController : Controller
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

        public JsonResult GetAllWithImages()
        {
            List<RoomTypes> result = ApiHelper<List<RoomTypes>>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypes/getallroomtypewithimages");
            return Json(new { result });
        }

        public JsonResult GetAllWithImagesAndFacilities()
        {
            List<RoomTypes> result = ApiHelper<List<RoomTypes>>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypes/getallroomtypewithimagesandfacilities");
            return Json(new { result });
        }

        public JsonResult Get(int id)
        {
            RoomType result = ApiHelper<RoomType>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypes/getbyid/{id}");
            return Json(new { result });
        }

        public JsonResult GetWithImagesAndFacilities(int id)
        {
            RoomType result = ApiHelper<RoomType>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypes/getbyidwithimagesandfacilities/{id}");
            return Json(new { result });
        }

        public JsonResult Delete(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypes/delete/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult Save([FromBody] CreateRoomTypeRequest model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/roomtypes/save", model);
            return Json(new { result });
        }

        public JsonResult Search([FromBody] SearchRoomTypesRequest model)
        {
            var result = ApiHelper<List<RoomTypeSearchResult>>.HttpPostAsync($"{Helper.ApiUrl}api/roomtypes/search", model);
            return Json(new { result });
        }
    }
}