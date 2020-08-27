using HotelBooking.Domain.Response;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelBooking.WEB.Controllers
{
    public class CustomersManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int id)
        {
            Customer result = ApiHelper<Customer>.HttpGetAsync($"{Helper.ApiUrl}api/customer/get/{id}");
            return Json(new { result });
        }

        public JsonResult GetAll()
        {
            List<Customer> result = ApiHelper<List<Customer>>.HttpGetAsync($"{Helper.ApiUrl}api/customer/get");
            return Json(new { result });
        }

        public JsonResult Delete(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/customer/delete/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult Save([FromBody] Customer model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/customer/save", model);
            return Json(new { result });
        }
    }
}