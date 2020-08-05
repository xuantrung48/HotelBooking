using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.HotelServices
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomType>> GetAll();
        Task<RoomType> GetById(int id);
        Task<ActionsResult> Save(RoomType roomType);
        Task<ActionsResult> Delete(int id);
    }
}