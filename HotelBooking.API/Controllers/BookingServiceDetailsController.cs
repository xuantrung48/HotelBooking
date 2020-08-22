using HotelBooking.BAL.Interface.Bookings;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.API.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    public class BookingServiceDetailsController : ControllerBase
    {
        private readonly IBookingServiceDetailsService bookingServiceDetailsService;

        public BookingServiceDetailsController(IBookingServiceDetailsService bookingServiceDetailsService)
        {
            this.bookingServiceDetailsService = bookingServiceDetailsService;
        }

        [HttpGet]
        [Route("api/bookingServiceDetails/get")]
        public async Task<IEnumerable<BookingServiceDetails>> Get()
        {
            return await bookingServiceDetailsService.Get();
        }

        [HttpGet]
        [Route("api/bookingServiceDetails/get/{id}")]
        public async Task<IEnumerable<BookingServiceDetails>> Get(int id)
        {
            return await bookingServiceDetailsService.Get(id);
        }

        [HttpPost]
        [Route("api/bookingServiceDetails/save")]
        public async Task<ActionsResult> Save(BookingServiceDetails bookingServiceDetails)
        {
            return await bookingServiceDetailsService.Save(bookingServiceDetails);
        }

        [HttpDelete]
        [Route("api/bookingServiceDetails/delete/{id}")]
        public async Task<ActionsResult> Remove(int id)
        {
            return await bookingServiceDetailsService.Delete(id);
        }

        [HttpDelete]
        [Route("api/bookingServiceDetails/deletebyBookingId/{id}")]
        public async Task<ActionsResult> DeleteByBookingId(int id)
        {
            return await bookingServiceDetailsService.DeleteByBookingId(id);
        }
    }
}