using Dapper;
using HotelBooking.DAL.Interface.Coupons;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Coupons;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Coupons
{
    public class CouponRepository : BaseRepository, ICouponRepository
    {
        public async Task<ActionsResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CouponId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "Coupon_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Coupon>> GetAll()
        {
            return await SqlMapper.QueryAsync<Coupon>(conn, "Coupon_GetAll", commandType: CommandType.StoredProcedure);
        }

        public async Task<Coupon> GetById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CouponId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<Coupon>(cnn: conn, sql: "Coupon_GetbyId", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Save(Coupon coupon)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CouponId", coupon.CouponId);
            parameters.Add("@CouponCode", coupon.CouponCode);
            parameters.Add("@Reduction", coupon.Reduction);
            parameters.Add("@Remain", coupon.Remain);
            parameters.Add("@EndDate", coupon.EndDate);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "Coupon_Save", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<CouponSearchResult> Search(string couponCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CouponCode", couponCode);
            return await SqlMapper.QueryFirstOrDefaultAsync<CouponSearchResult>(cnn: conn, sql: "Coupon_Search", param: parameters, commandType: CommandType.StoredProcedure);
        }
    }
}