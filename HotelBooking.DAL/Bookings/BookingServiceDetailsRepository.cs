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
        public async Task<IEnumerable<BookingServiceDetails>> Get(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingId", id);
            return await SqlMapper.QueryAsync<BookingServiceDetails>(cnn: conn, sql: "BookingServiceDetails_GetByBookingId", param: parameters, commandType: CommandType.StoredProcedure);
        }

        //public async Task<IEnumerable<BookingServiceDetails>> Get()
        //{
        //    return await SqlMapper.QueryAsync<BookingServiceDetails>(conn, "GetBookingsServiceDetails", commandType: CommandType.StoredProcedure);
        //}

        public async Task<ActionsResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingServiceDetailsId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "BookingServiceDetails_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Save(BookingServiceDetails bookingServiceDetails)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BookingId", bookingServiceDetails.BookingId);
                parameters.Add("@ServiceId", bookingServiceDetails.ServiceId);
                parameters.Add("@ServiceQuantity", bookingServiceDetails.ServiceQuantity);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "BookingServiceDetails_Save", param: parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                return new ActionsResult()
                {
                    Id = 0,
                    Message = "Có lỗi xảy ra, xin thử lại!"
                };
            }
        }

        public async Task<IEnumerable<BookingServiceDetails>> Get()
        {
            return await SqlMapper.QueryAsync<BookingServiceDetails>(cnn: conn, sql: "BookingServiceDetails_GetAll", commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> DeleteByBookingId(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "BookingServiceDetails_DeletebyBookingId", param: parameters, commandType: CommandType.StoredProcedure);
        }
    }
}