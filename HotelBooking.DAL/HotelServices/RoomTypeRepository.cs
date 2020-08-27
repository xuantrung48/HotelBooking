using Dapper;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.Domain.Request.Search;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HotelBooking.DAL.HotelServices
{
    public class RoomTypeRepository : BaseRepository, IRoomTypeRepository
    {
        public async Task<RoomType> GetById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@RoomTypeId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<RoomType>(cnn: conn, sql: "RoomType_GetbyId", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<RoomType>> GetAll()
        {
            return await SqlMapper.QueryAsync<RoomType>(conn, "RoomType_GetAll", commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@RoomTypeId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "RoomType_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Save(RoomType roomType)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@RoomTypeId", roomType.RoomTypeId);
                parameters.Add("@Name", roomType.Name);
                parameters.Add("@DefaultPrice", roomType.DefaultPrice);
                parameters.Add("@MaxAdult", roomType.MaxAdult);
                parameters.Add("@MaxChildren", roomType.MaxChildren);
                parameters.Add("@MaxPeople", roomType.MaxPeople);
                parameters.Add("@Quantity", roomType.Quantity);
                parameters.Add("@Description", roomType.Description);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "RoomType_Save", param: parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<RoomTypeSearchResult>> Search(SearchModel request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Adult", request.NumberofAdults);
            parameters.Add("@Children", request.NumberofChildren);
            parameters.Add("@CheckInDate", request.CheckinDate);
            parameters.Add("@CheckOutDate", request.CheckoutDate);
            return await SqlMapper.QueryAsync<RoomTypeSearchResult>(cnn: conn, sql: "RoomType_Search", param: parameters, commandType: CommandType.StoredProcedure);
        }
    }
}