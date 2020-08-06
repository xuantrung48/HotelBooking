using HotelBooking.BAL.Interface.Bookings;
using HotelBooking.DAL.Interface.Bookings;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        public Task<ActionsResult> Delete(int id)
        {
            return bookingRepository.Delete(id);
        }

        public Task<IEnumerable<Booking>> Get()
        {
            return bookingRepository.Get();
        }

        public Task<Booking> Get(int id)
        {
            return bookingRepository.Get(id);
        }

        public Task<ActionsResult> Save(Booking booking)
        {
            return bookingRepository.Save(booking);
        }
    }
}