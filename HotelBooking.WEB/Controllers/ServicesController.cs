using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.WEB.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}