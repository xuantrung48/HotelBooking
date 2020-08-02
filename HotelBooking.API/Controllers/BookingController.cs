using HotelBooking.BAL.Interface;
using HotelBooking.BAL.Interface.Bookings;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionResult = HotelBooking.Domain.Response.ActionResult;

namespace HotelBooking.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;
        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpGet]
        [Route("api/booking/get")]
        public async Task<IEnumerable<Booking>> Get()
        {
            return await bookingService.Get();
        }

        [HttpGet]
        [Route("api/booking/get/{id}")]
        public async Task<Booking> Get(int id)
        {
            return await bookingService.Get(id);
        }  
        
        [HttpPost]
        [Route("api/booking/save")]
        public async Task<ActionResult> Save(Booking booking)
        {
            return await bookingService.Save(booking);
        }

        [HttpDelete]
        [Route("api/booking/delete/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            return await bookingService.Delete(id);
        }
    }
}
