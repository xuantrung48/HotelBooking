using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.Bookings
{
    public interface IBookingRoomDetailsService
    {
        Task<IEnumerable<BookingRoomDetails>> Get();
        Task<BookingRoomDetails> Get(int id);
        Task<ActionResult> Save(BookingRoomDetails bookingRoomDetails);
        Task<ActionResult> Delete(int id);
    }
}
