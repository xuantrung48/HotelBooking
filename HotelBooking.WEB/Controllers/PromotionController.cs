﻿using HotelBooking.Domain.Response.Promotions;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Web.Ultilities;
using System.Collections.Generic;
using ActionsResult = HotelBooking.Domain.Response.ActionsResult;

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
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/promotions/delete/{id}", "DELETE");
            return Json(new { result });
        }
        public JsonResult Save([FromBody] Promotion model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/promotions/save", model);
            return Json(new { result });
        }
    }
}
