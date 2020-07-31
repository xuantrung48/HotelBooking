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
        public async Task<BookingRoomDetails> Get(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<BookingRoomDetails>(cnn: conn, sql: "GetBookingRoomDetails", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<BookingRoomDetails>> Get()
        {
            return await SqlMapper.QueryAsync<BookingRoomDetails>(conn, "GetBookingsRoomDetails", commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookingRoomDetailsId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionResult>(cnn: conn, sql: "DeleteBookingRoomDetails", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionResult> Save(BookingRoomDetails bookingRoomDetails)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BookingId", bookingRoomDetails.BookingId);
                parameters.Add("@RoomTypeId", bookingRoomDetails.RoomTypeId);
                parameters.Add("@CheckInDate", bookingRoomDetails.CheckInDate);
                parameters.Add("@CheckOutDate", bookingRoomDetails.CheckOutDate);
                parameters.Add("@RoomPrice", bookingRoomDetails.RoomPrice);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionResult>(cnn: conn, sql: "SaveBookingRoomDetails", param: parameters, commandType: CommandType.StoredProcedure);
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
