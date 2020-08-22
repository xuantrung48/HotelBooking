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
    public class BookingRoomDetailsRepository : BaseRepository, IBookingRoomDetailsRepository
    {
        public async Task<IEnumerable<BookingRoomDetails>> Get(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingId", id);
            return await SqlMapper.QueryAsync<BookingRoomDetails>(cnn: conn, sql: "BookingRoomDetails_GetByBookingId", param: parameters, commandType: CommandType.StoredProcedure);
        }

        //public async Task<IEnumerable<BookingRoomDetails>> Get()
        //{
        //    return await SqlMapper.QueryAsync<BookingRoomDetails>(conn, "GetBookingsRoomDetails", commandType: CommandType.StoredProcedure);
        //}

        public async Task<ActionsResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingRoomDetailsId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "BookingRoomDetails_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Save(BookingRoomDetails bookingRoomDetails)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BookingId", bookingRoomDetails.BookingId);
                parameters.Add("@RoomTypeId", bookingRoomDetails.RoomTypeId);
                parameters.Add("@RoomQuantity", bookingRoomDetails.RoomQuantity);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "BookingRoomDetails_Save", param: parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<BookingRoomDetails>> Display(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingId", id);
            return await SqlMapper.QueryAsync<BookingRoomDetails>(cnn: conn, sql: "BookingRoomDetails_DisplayBookingRoomTypesByBookingId", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> DeleteByBookingId(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "BookingRoomDetails_DeletebyBookingId", param: parameters, commandType: CommandType.StoredProcedure);
        }
    }
}