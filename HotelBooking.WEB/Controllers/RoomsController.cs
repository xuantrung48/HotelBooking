using Microsoft.AspNetCore.Mvc;

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