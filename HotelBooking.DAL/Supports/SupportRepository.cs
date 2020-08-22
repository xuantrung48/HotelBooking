using Dapper;
using HotelBooking.DAL.Interface.Supports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Supports
{
    public class SupportRepository : BaseRepository, ISupportRepository
    {
        public async Task<IEnumerable<DateTime>> CreateTableDateAsync(DateTime startDate, DateTime endDate)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@StartDate", startDate);
            parameters.Add("@EndDate", endDate);
            return await SqlMapper.QueryAsync<DateTime>(cnn: conn, sql: "Supports_CreateDateTable", param: parameters, commandType: CommandType.StoredProcedure);
        }
    }
}