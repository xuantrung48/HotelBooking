using System.Data;
using System.Data.SqlClient;

namespace HotelBooking.DAL
{
    public class BaseRepository
    {
        protected IDbConnection conn;
        public BaseRepository()
        {
            var connectionString = "Data Source=DESKTOP-5V9HDF2\\SQLEXPRESS01;Initial Catalog=Hotel2;Integrated Security=True";
            conn = new SqlConnection(connectionString);
        }
    }
}