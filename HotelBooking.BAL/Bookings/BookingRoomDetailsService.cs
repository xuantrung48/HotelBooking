using HotelBooking.BAL.Interface.Bookings;
using HotelBooking.DAL.Interface.Bookings;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Bookings
{
    public class BookingRoomDetailsService : IBookingRoomDetailsService
    {
        private readonly IBookingRoomDetailsRepository bookingRoomDetailsRepository;

        public BookingRoomDetailsService(IBookingRoomDetailsRepository bookingRoomDetailsRepository)
        {
            this.bookingRoomDetailsRepository = bookingRoomDetailsRepository;
        }

        public async Task<IEnumerable<BookingRoomDetails>> Get(int id)
        {
            return await bookingRoomDetailsRepository.Get(id);
        }

        public async Task<IEnumerable<BookingRoomDetails>> Display(int id)
        {
            return await bookingRoomDetailsRepository.Display(id);
        }

        public async Task<ActionsResult> Delete(int id)
        {
            return await bookingRoomDetailsRepository.Delete(id);
        }

        public async Task<ActionsResult> Save(BookingRoomDetails bookingRoomDetails)
        {
            return await bookingRoomDetailsRepository.Save(bookingRoomDetails);
        }

        public async Task<ActionsResult> DeleteByBookingId(int id)
        {
            return await bookingRoomDetailsRepository.DeleteByBookingId(id);
        }
    }
}