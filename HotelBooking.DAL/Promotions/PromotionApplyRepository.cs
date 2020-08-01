using Dapper;
using HotelBooking.DAL.Interface.Promotions;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Promotions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Promotions
{
    public class PromotionApplyRepository : BaseRepository, IPromotionApplyRepository
    {

        public async Task<IEnumerable<PromotionApply>> GetAll()
        {
            return await SqlMapper.QueryAsync<PromotionApply>(conn, "PromotionApply_GetAll", commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PromotionApplyId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionResult>(cnn: conn, sql: "PromotionApply_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionResult> Save(PromotionApply promotionApply)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PromotionApplyId", promotionApply.PromotionApplyId);
                parameters.Add("@PromotionId", promotionApply.PromotionId);
                parameters.Add("@RoomTypeId", promotionApply.RoomTypeId);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionResult>(cnn: conn, sql: "PromotionApply_Save", param: parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<PromotionApply> GetByRoomTypeId(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@RoomTypeId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<PromotionApply>(cnn: conn, sql: "PromotionApply_GetByRoomTypeId", param: parameters, commandType: CommandType.StoredProcedure);
        }
    }
}