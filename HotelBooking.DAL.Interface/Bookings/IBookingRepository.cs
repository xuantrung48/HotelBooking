using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.Bookings
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> Get();

        Task<Booking> Get(int id);

        Task<ActionsResult> Save(Booking booking);

        Task<ActionsResult> Delete(int id);

        Task<IEnumerable<DateTime>> GetListDate(int id);
    }
}