using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.HotelServices
{
    public interface IRoomTypeImageRepository
    {
        Task<ActionsResult> Save(UploadRoomTypeImagesRequest roomTypeImagesRequest);

        Task<IEnumerable<RoomTypeImage>> GetByRoomTypeId(int id);

        Task<ActionsResult> Delete(int id);
    }
}