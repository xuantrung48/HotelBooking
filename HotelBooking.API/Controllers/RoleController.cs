using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}