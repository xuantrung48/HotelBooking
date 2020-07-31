using Dapper;
using HotelBooking.DAL.Interface.Bookings;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Bookings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Bookings
{
    public class BookingServiceDetailsRepository : BaseRepository, IBookingServiceDetailsRepository
    {
        public async Task<BookingServiceDetails> Get(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingServiceDetailsId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<BookingServiceDetails>(cnn: conn, sql: "GetBookingServiceDetails", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<BookingServiceDetails>> Get()
        {
            return await SqlMapper.QueryAsync<BookingServiceDetails>(conn, "GetBookingsServiceDetails", commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingServiceDetailsId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionResult>(cnn: conn, sql: "DeleteBookingServiceDetails", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionResult> Save(BookingServiceDetails bookingServiceDetails)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BookingId", bookingServiceDetails.BookingId);
                parameters.Add("@ServiceId", bookingServiceDetails.ServiceId);
                parameters.Add("@ServiceQuantity", bookingServiceDetails.ServiceQuantity);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionResult>(cnn: conn, sql: "SaveBookingServiceDetails", param: parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                return new ActionResult()
                {
                    Id = 0,
                    Message = "Có lỗi xảy ra, xin thử lại!"
                };
            }
        }
    }
}