using HotelBooking.Domain.Request.Search;
using HotelBooking.Domain.Response.Search;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.WEB.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Search([FromBody] SearchRequest request)
        {
            var result = ApiHelper<SearchResult>.HttpPostAsync($"{Helper.ApiUrl}api/Search", request);
            return Json(new { result });
        }
        //public JsonResult GetRoomType(int id, int minRemain)
        //{
        //    var listRoomType = ApiHelper<List<RoomType>>.HttpGetAsync($"{Helper.ApiUrl}api/roomtypes/getall");
        //    var result = (from
        //    return Json(new { result });
        //}
        public IActionResult BookingDetails()
        {
            
            return View();
        }
    }
}