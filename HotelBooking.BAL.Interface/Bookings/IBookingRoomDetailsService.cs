using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.Bookings
{
    public interface IBookingRoomDetailsService
    {
        Task<IEnumerable<BookingRoomDetails>> Display(int id);

        Task<IEnumerable<BookingRoomDetails>> Get(int id);

        Task<ActionsResult> Save(BookingRoomDetails bookingRoomDetails);

        Task<ActionsResult> Delete(int id);

        Task<ActionsResult> DeleteByBookingId(int id);
    }
}