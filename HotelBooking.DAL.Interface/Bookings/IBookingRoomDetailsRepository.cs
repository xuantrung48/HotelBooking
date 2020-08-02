using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.Bookings
{
    public interface IBookingRoomDetailsRepository
    {
        //Task<IEnumerable<BookingRoomDetails>> Get();
        Task<IEnumerable<BookingRoomDetails>> Get(int id);
        Task<ActionResult> Save(BookingRoomDetails bookingRoomDetails);
        Task<ActionResult> Delete(int id);
    }
}
