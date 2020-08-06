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
    public class PromotionRepository : BaseRepository, IPromotionRepository
    {
        public async Task<Promotion> GetById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PromotionId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<Promotion>(cnn: conn, sql: "Promotion_GetById", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Promotion>> GetAll()
        {
            return await SqlMapper.QueryAsync<Promotion>(conn, "Promotion_GetAll", commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PromotionId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "Promotion_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Save(Promotion promotion)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PromotionId", promotion.PromotionId);
                parameters.Add("@PromotionName", promotion.PromotionName);
                parameters.Add("@StartDate", promotion.StartDate);
                parameters.Add("@EndDate", promotion.EndDate);
                parameters.Add("@DiscountRates", promotion.DiscountRates);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "Promotion_Save", param: parameters, commandType: CommandType.StoredProcedure);
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