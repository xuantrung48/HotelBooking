using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.HotelServices
{
    public interface IRoomTypeImageService
    {
        Task<IEnumerable<RoomTypeImage>> GetByRoomTypeId(int id);

        Task<ActionsResult> Save(UploadRoomTypeImagesRequest roomTypeImage);

        Task<ActionsResult> Delete(int id);
    }
}