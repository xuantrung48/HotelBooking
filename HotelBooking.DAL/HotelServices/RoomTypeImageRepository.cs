using Dapper;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HotelBooking.DAL.HotelServices
{
    public class RoomTypeImageRepository : BaseRepository, IRoomTypeImageRepository
    {
        public async Task<IEnumerable<RoomTypeImage>> GetByRoomTypeId(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@RoomTypeId", id);
            return await SqlMapper.QueryAsync<RoomTypeImage>(conn, "RoomTypeImage_GetByRoomTypeId", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@RoomTypeImageId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "RoomTypeImage_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Save(UploadRoomTypeImagesRequest uploadRoomTypeImagesRequest)
        {
            try
            {
                var result = new ActionsResult();
                foreach (var imgData in uploadRoomTypeImagesRequest.Images)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@RoomTypeId", uploadRoomTypeImagesRequest.RoomTypeId);
                    parameters.Add("@ImageData", imgData);
                    result = await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "RoomTypeImage_Save", param: parameters, commandType: CommandType.StoredProcedure);
                }
                return result;
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
    }
}