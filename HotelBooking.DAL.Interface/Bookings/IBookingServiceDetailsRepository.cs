using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.Bookings
{
    public interface IBookingServiceDetailsRepository
    {
        Task<IEnumerable<BookingServiceDetails>> Get();

        Task<IEnumerable<BookingServiceDetails>> Get(int id);

        Task<ActionsResult> Save(BookingServiceDetails bookingServiceDetails);

        Task<ActionsResult> Delete(int id);

        Task<ActionsResult> DeleteByBookingId(int id);
    }
}