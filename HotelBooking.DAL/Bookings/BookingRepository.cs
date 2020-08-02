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
    public class BookingRepository : BaseRepository, IBookingRepository
    {
        public async Task<Booking> Get(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<Booking>(cnn: conn, sql: "Booking_GetByBookingId", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Booking>> Get()
        {
            return await SqlMapper.QueryAsync<Booking>(cnn: conn, sql: "Booking_GetAll", commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionResult>(cnn: conn, sql: "Booking_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionResult> Save(Booking booking)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BookingId", booking.BookingId);
                parameters.Add("@CustomerId", booking.CustomerId);
                parameters.Add("@CreateDate", booking.CreateDate);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionResult>(cnn: conn, sql: "Booking_Save", param: parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                return new ActionResult()
                {
                    Id = 0,
                    Message = e.Message
                    //Message = "Có lỗi xảy ra, xin thử lại!"
                };
            }
        }
    }
}
