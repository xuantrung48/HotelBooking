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
    public class BookingRoomDetailsController : ControllerBase
    {
        private readonly IBookingRoomDetailsService bookingRoomDetailsService;

        public BookingRoomDetailsController(IBookingRoomDetailsService bookingRoomDetailsService)
        {
            this.bookingRoomDetailsService = bookingRoomDetailsService;
        }

        [HttpGet]
        [Route("api/bookingRoomDetails/bookingRoomDetails_DisplayBookingRoomTypesByBookingId/{id}")]
        public async Task<IEnumerable<BookingRoomDetails>> Display(int id)
        {
            return await bookingRoomDetailsService.Display(id);
        }

        [HttpGet]
        [Route("api/bookingRoomDetails/get/{id}")]
        public async Task<IEnumerable<BookingRoomDetails>> Get(int id)
        {
            return await bookingRoomDetailsService.Get(id);
        }

        [HttpPost]
        [Route("api/bookingRoomDetails/save")]
        public async Task<ActionsResult> Save(BookingRoomDetails bookingRoomDetails)
        {
            return await bookingRoomDetailsService.Save(bookingRoomDetails);
        }

        [HttpDelete]
        [Route("api/bookingRoomDetails/delete/{id}")]
        public async Task<ActionsResult> Remove(int id)
        {
            return await bookingRoomDetailsService.Delete(id);
        }

        [HttpDelete]
        [Route("api/bookingRoomDetails/detetebyBookingId/{id}")]
        public async Task<ActionsResult> DeleteByBookingId(int id)
        {
            return await bookingRoomDetailsService.DeleteByBookingId(id);
        }
    }
}