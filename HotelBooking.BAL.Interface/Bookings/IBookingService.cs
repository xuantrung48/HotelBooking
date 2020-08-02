using HotelBooking.DAL.Interface.Bookings;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.Bookings
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> Get();
        Task<Booking> Get(int id);
        Task<ActionResult> Save(Booking booking);
        Task<ActionResult> Delete(int id);
    }
}
