using HotelBooking.Domain;
using System.Data;
using System.Data.SqlClient;

namespace HotelBooking.DAL
{
    public class BaseRepository
    {
        protected IDbConnection conn;

        public BaseRepository()
        {
            var connectionString = Common.ConnectionString;
            conn = new SqlConnection(connectionString);
        }
    }
}