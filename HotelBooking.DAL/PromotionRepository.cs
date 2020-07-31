using Dapper;
using HotelBooking.DAL.Interface;
using HotelBooking.Domain.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.DAL
{
    public class PromotionRepository : BaseRepository, IPromotionRepository
    {
        public async Task<Promotion> GetByRoomTypeId(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@RoomTypeId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<Promotion>(cnn: conn, sql: "Promotion_GetbyRoomTypeId", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Promotion>> GetAll()
        {
            return await SqlMapper.QueryAsync<Promotion>(conn, "Promotion_GetAll", commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PromotionId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionResult>(cnn: conn, sql: "Promotion_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionResult> Save(Promotion promotion)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PromotionId", promotion.PromotionId);
                parameters.Add("@RoomTypeId", promotion.RoomTypeId);
                parameters.Add("@StartDate", promotion.StartDate);
                parameters.Add("@EndDate", promotion.EndDate);
                parameters.Add("@DiscountRates", promotion.DiscountRates);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionResult>(cnn: conn, sql: "Promotion_Save", param: parameters, commandType: CommandType.StoredProcedure);
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