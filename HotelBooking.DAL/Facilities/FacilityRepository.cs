using Dapper;
using HotelBooking.DAL.Interface.Facilities;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Facilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Facilities
{
    public class FacilityRepository : BaseRepository, IFacilityRepository
    {
        public async Task<Facility> GetById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FacilityId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<Facility>(cnn: conn, sql: "Facility_GetbyId", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Facility>> GetAll()
        {
            return await SqlMapper.QueryAsync<Facility>(conn, "Facility_GetAll", commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FacilityId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "Facility_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Save(Facility facility)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FacilityId", facility.FacilityId);
                parameters.Add("@FacilityName", facility.FacilityName);
                parameters.Add("@FacilityImage", facility.FacilityImage);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "Facility_Save", param: parameters, commandType: CommandType.StoredProcedure);
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