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
    public class ServiceImageRepository : BaseRepository, IServiceImageRepository
    {
        public async Task<IEnumerable<ServiceImage>> GetByServiceId(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ServiceId", id);
            return await SqlMapper.QueryAsync<ServiceImage>(conn, "ServiceImage_GetByServiceId", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ServiceImageId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "ServiceImage_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Save(UploadServiceImagesRequest uploadServiceImagesRequest)
        {
            try
            {
                var result = new ActionsResult();
                foreach (var imgData in uploadServiceImagesRequest.Images)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ServiceId", uploadServiceImagesRequest.ServiceId);
                    parameters.Add("@ImageData", imgData);
                    result = await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "ServiceImage_Save", param: parameters, commandType: CommandType.StoredProcedure);
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