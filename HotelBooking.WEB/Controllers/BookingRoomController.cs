using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.WEB.Controllers
{
    public class BookingRoomController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
