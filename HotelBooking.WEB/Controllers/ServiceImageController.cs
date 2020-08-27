using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelBooking.WEB.Controllers
{
    public class ServiceImageController : Controller
    {
        public JsonResult GetByServiceId(int id)
        {
            List<ServiceImage> result = ApiHelper<List<ServiceImage>>.HttpGetAsync($"{Helper.ApiUrl}api/serviceimages/getbyserviceid/{id}");
            return Json(new { result });
        }

        public JsonResult Delete(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/serviceimages/delete/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult Save([FromBody] ServiceImage model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/serviceimages/save", model);
            return Json(new { result });
        }
    }
}