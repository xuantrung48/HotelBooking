using HotelBooking.BAL.Interface.Bookings;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionsResult> Save(Booking booking)
        {
            return await bookingService.Save(booking);
        }

        [HttpDelete]
        [Route("api/booking/delete/{id}")]
        public async Task<ActionsResult> Remove(int id)
        {
            return await bookingService.Delete(id);
        }

        [HttpGet]
        [Route("api/booking/getListDate/{id}")]
        public async Task<IEnumerable<DateTime>> GetListDate(int id)
        {
            return await bookingService.GetListDate(id);
        }
    }
}