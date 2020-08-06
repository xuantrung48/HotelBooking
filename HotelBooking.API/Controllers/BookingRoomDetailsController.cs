﻿using HotelBooking.BAL.Interface.Bookings;
using HotelBooking.Domain.Response.Bookings;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionsResult = HotelBooking.Domain.Response.ActionsResult;

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

        //[HttpGet]
        //[Route("api/booking/get")]
        //public async Task<IEnumerable<Booking>> Get()
        //{
        //    return await bookingRoomDetails.Get();
        //}

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
    }
}
