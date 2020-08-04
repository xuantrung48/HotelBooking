using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.Domain.Response.Promotions;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Web.Ultilities;

namespace HotelBooking.WEB.Controllers
{
    public class PromotionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult Get(int id)
        {
            Promotion result = ApiHelper<Promotion>.HttpGetAsync($"{Helper.ApiUrl}api/promotions/getbyid/{id}");
            return Json(new { result });
        }
        public JsonResult GetAll()
        {
            List<Promotion> result = ApiHelper<List<Promotion>>.HttpGetAsync($"{Helper.ApiUrl}api/promotions/getall");
            return Json(new { result });
        }

        public JsonResult Delete(int id)
        {
            ActionResult result = ApiHelper<ActionResult>.HttpGetAsync($"{Helper.ApiUrl}api/promotions/delete/{id}", "DELETE");
            return Json(new { result });
        }
        public JsonResult Save([FromBody] Promotion model)
        {
            ActionResult result;
            result = ApiHelper<ActionResult>.HttpPostAsync($"{Helper.ApiUrl}api/promotions/save", model);
            return Json(new { result });
        }
    }
}
