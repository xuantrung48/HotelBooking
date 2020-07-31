using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.HotelServices
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomType>> Get();
        Task<RoomType> Get(int id);
        Task<ActionResult> Save(RoomType roomType);
        Task<ActionResult> Delete(int id);
    }
}