using Dapper;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HotelBooking.DAL.HotelServices
{
    public class ServiceRepository : BaseRepository, IServiceRepository
    {
        public async Task<Service> Get(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ServiceId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<Service>(cnn: conn, sql: "GetService", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Service>> Get()
        {
            return await SqlMapper.QueryAsync<Service>(conn, "GetServices", commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ServiceId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionResult>(cnn: conn, sql: "DeleteService", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionResult> Save(Service service)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ServiceId", service.ServiceId);
                parameters.Add("@ServiceName", service.ServiceName);
                parameters.Add("@Price", service.Price);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionResult>(cnn: conn, sql: "SaveService", param: parameters, commandType: CommandType.StoredProcedure);
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
