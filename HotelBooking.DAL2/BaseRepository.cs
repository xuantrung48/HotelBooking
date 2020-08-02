using System.Data;
using System.Data.SqlClient;

namespace HotelBooking.DAL
{
    public class BaseRepository
    {
        protected IDbConnection conn;
        public BaseRepository()
        {
            var connectionString = "workstation id=CoCoHomeStayDb.mssql.somee.com;packet size=4096;user id=quangnguyencg1_SQLLogin_1;pwd=r18z5w2tqk;data source=CoCoHomeStayDb.mssql.somee.com;persist security info=False;initial catalog=CoCoHomeStayDb";
            conn = new SqlConnection(connectionString);
        }
    }
}