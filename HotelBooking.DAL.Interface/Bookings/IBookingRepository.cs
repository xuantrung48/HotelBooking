using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.Bookings
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> Get();
        Task<Booking> Get(int id);
        Task<ActionResult> Save(Booking booking);
        Task<ActionResult> Delete(int id);
    }
}
