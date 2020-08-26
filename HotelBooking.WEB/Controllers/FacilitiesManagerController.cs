using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Facilities;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelBooking.WEB.Controllers
{
    public class FacilitiesManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetAll()
        {
            List<Facility> result = ApiHelper<List<Facility>>.HttpGetAsync($"{Helper.ApiUrl}api/facilities/getall");
            return Json(new { result });
        }

        public JsonResult Get(int id)
        {
            Facility result = ApiHelper<Facility>.HttpGetAsync($"{Helper.ApiUrl}api/facilities/getbyid/{id}");
            return Json(new { result });
        }

        public JsonResult Delete(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/facilities/delete/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult Save([FromBody] Facility model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/facilities/save", model);
            return Json(new { result });
        }
    }
}