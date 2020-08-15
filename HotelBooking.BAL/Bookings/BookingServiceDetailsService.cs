using HotelBooking.BAL.Interface.Bookings;
using HotelBooking.DAL.Interface.Bookings;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Bookings
{
    public class BookingServiceDetailsService : IBookingServiceDetailsService
    {
        private readonly IBookingServiceDetailsRepository bookingServiceDetailsRepository;

        public BookingServiceDetailsService(IBookingServiceDetailsRepository bookingServiceDetailsRepository)
        {
            this.bookingServiceDetailsRepository = bookingServiceDetailsRepository;
        }

        public async Task<IEnumerable<BookingServiceDetails>> Get(int id)
        {
            return await bookingServiceDetailsRepository.Get(id);
        }

        public async Task<IEnumerable<BookingServiceDetails>> Get()
        {
            return await bookingServiceDetailsRepository.Get();
        }

        public async Task<ActionsResult> Delete(int id)
        {
            return await bookingServiceDetailsRepository.Delete(id);
        }

        public async Task<ActionsResult> Save(BookingServiceDetails bookingServiceDetails)
        {
            return await bookingServiceDetailsRepository.Save(bookingServiceDetails);
        }

        public async Task<ActionsResult> DeleteByBookingId(int id)
        {
            return await bookingServiceDetailsRepository.DeleteByBookingId(id);
        }
    }
}