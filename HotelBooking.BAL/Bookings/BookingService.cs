using HotelBooking.BAL.Interface.Bookings;
using HotelBooking.DAL.Interface;
using HotelBooking.DAL.Interface.Bookings;
using HotelBooking.DAL.Interface.Coupons;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IBookingRoomDetailsRepository bookingRoomDetailsRepository;
        private readonly IBookingServiceDetailsRepository bookingServiceDetailsRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ICouponRepository couponRepository;

        public BookingService(
             IBookingRepository bookingRepository
            ,IBookingRoomDetailsRepository bookingRoomDetailsRepository
            ,IBookingServiceDetailsRepository bookingServiceDetailsRepository
            ,ICustomerRepository customerRepository
            ,ICouponRepository couponRepository)
        {
            this.bookingRepository = bookingRepository;
            this.bookingRoomDetailsRepository = bookingRoomDetailsRepository;
            this.bookingServiceDetailsRepository = bookingServiceDetailsRepository;
            this.customerRepository = customerRepository;
            this.couponRepository = couponRepository;
        }

        public Task<ActionsResult> Delete(int id)
        {
            return bookingRepository.Delete(id);
        }

        public async Task<IEnumerable<Booking>> Get()
        {

            var bookings = (await bookingRepository.Get()).ToList();
            var customers = await customerRepository.Get();
            var coupons = await couponRepository.GetAll();
            var data = (from b in bookings
                        join c in customers
                        on b.CustomerId equals c.CustomerId
                        join cp in coupons
                        on b.CouponId equals cp.CouponId into temp
                        from subtemp in temp.DefaultIfEmpty()
                        select new Booking
                        {
                            BookingId = b.BookingId,
                            CreateDate = b.CreateDate,
                            IsCanceled = b.IsCanceled,
                            CustomerId = c.CustomerId,
                            BookingCustomer = c,
                            CouponId = b.CouponId,
                            BookingCoupon = subtemp,
                            bookingRoomDetails = b.bookingRoomDetails,
                            bookingServiceDetails = b.bookingServiceDetails,
                            RoomAmount = b.RoomAmount,
                            ServiceAmount = b.ServiceAmount,
                        });
            return data;
        }

        public async Task<Booking> Get(int id)
        {
            
            var booking = await bookingRepository.Get(id);
            var customer = await customerRepository.Get(booking.CustomerId);
            var coupon = await couponRepository.GetById(booking.CouponId.GetValueOrDefault());
            var data = new Booking()
            {
                BookingId = booking.BookingId,
                CreateDate = booking.CreateDate,
                IsCanceled = booking.IsCanceled,
                CustomerId = customer.CustomerId,
                BookingCustomer = customer,
                CouponId = booking.CouponId,
                BookingCoupon = coupon,
                bookingRoomDetails = booking.bookingRoomDetails,
                bookingServiceDetails = booking.bookingServiceDetails,
                RoomAmount = booking.RoomAmount,
                ServiceAmount = booking.ServiceAmount,
            };
            return data;
        }

        public Task<ActionsResult> Save(Booking booking)
        {
            throw new System.NotImplementedException();
        }
        //public Task<ActionsResult> Save(Booking booking)
        //{

        //}
    }
}