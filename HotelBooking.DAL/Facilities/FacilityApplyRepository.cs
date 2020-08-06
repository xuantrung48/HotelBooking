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
    public class FacilityApplyRepository : BaseRepository, IFacilityApplyRepository
    {
        public async Task<ActionsResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FacilityApplyId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "FacilityApply_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<ActionsResult> DeleteByRoomTypeId(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@RoomTypeId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "FacilityApply_DeleteByRoomTypeId", param: parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<ActionsResult> Save(FacilityApply facilityApply)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FacilityId", facilityApply.FacilityId);
                parameters.Add("@RoomTypeId", facilityApply.RoomTypeId);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "FacilityApply_Save", param: parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<FacilityApply>> GetByRoomTypeId(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@RoomTypeId", id);
            return await SqlMapper.QueryAsync<FacilityApply>(cnn: conn, sql: "FacilityApply_GetByRoomTypeId", param: parameters, commandType: CommandType.StoredProcedure);
        }
    }
}