﻿using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.Bookings
{
    public interface IBookingServiceDetailsService
    {
        //Task<IEnumerable<BookingServiceDetails>> Get();
        Task<IEnumerable<BookingServiceDetails>> Get(int id);
        Task<ActionResult> Save(BookingServiceDetails bookingServiceDetails);
        Task<ActionResult> Delete(int id);
    }
}