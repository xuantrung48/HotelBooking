using HotelBooking.Domain.Response.HotelServices;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Web.Ultilities;
using System.Collections.Generic;

namespace HotelBooking.WEB.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            return View(id);
        }
    }
}
