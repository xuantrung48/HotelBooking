﻿using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Web.Ultilities;
using System.Collections.Generic;

namespace HotelBooking.WEB.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int id)
        {
            Service result = ApiHelper<Service>.HttpGetAsync($"{Helper.ApiUrl}api/service/get/{id}");
            return Json(new { result });
        }

        public JsonResult GetAll()
        {
            IEnumerable<Service> result = ApiHelper<IEnumerable<Service>>.HttpGetAsync($"{Helper.ApiUrl}api/service/get");
            return Json(new { result });
        }
        public JsonResult GetWithImages(int id)
        {
            Service result = ApiHelper<Service>.HttpGetAsync($"{Helper.ApiUrl}api/service/getbyidwithimages/{id}");
            return Json(new { result });
        }
        public JsonResult Delete(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/service/delete/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult Save([FromBody] CreateServiceRequest model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/service/save", model);
            return Json(new { result });
        }
    }
}