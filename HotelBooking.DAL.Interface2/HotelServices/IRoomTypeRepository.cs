using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.HotelServices
{
    public interface IRoomTypeRepository
    {
        Task<IEnumerable<RoomType>> GetAll();
        Task<RoomType> GetById(int id);
        Task<ActionResult> Save(RoomType roomType);
        Task<ActionResult> Delete(int id);
    }
}