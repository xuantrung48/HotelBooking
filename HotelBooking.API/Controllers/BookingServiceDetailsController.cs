﻿using HotelBooking.BAL.Interface;
using HotelBooking.BAL.Interface.Bookings;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionResult = HotelBooking.Domain.Response.ActionResult;

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

        //[HttpGet]
        //[Route("api/booking/get")]
        //public async Task<IEnumerable<Booking>> Get()
        //{
        //    return await bookingRoomDetails.Get();
        //}

        [HttpGet]
        [Route("api/bookingServiceDetails/get/{id}")]
        public async Task<IEnumerable<BookingServiceDetails>> Get(int id)
        {
            return await bookingServiceDetailsService.Get(id);
        }  
        
        [HttpPost]
        [Route("api/bookingServiceDetails/save")]
        public async Task<ActionResult> Save(BookingServiceDetails bookingServiceDetails)
        {
            return await bookingServiceDetailsService.Save(bookingServiceDetails);
        }

        [HttpDelete]
        [Route("api/bookingServiceDetails/delete/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            return await bookingServiceDetailsService.Delete(id);
        }
    }
}
