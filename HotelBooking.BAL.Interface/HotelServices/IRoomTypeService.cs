using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Request.Search;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.HotelServices
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomType>> GetAll();

        Task<IEnumerable<RoomTypes>> GetAllRoomTypeWithImages();

        Task<IEnumerable<RoomTypes>> GetAllRoomTypeWithImagesAndFacilities();

        Task<RoomType> GetById(int id);

        Task<RoomType> GetByIdWithImagesAndFacilities(int id);

        Task<ActionsResult> Save(CreateRoomTypeRequest roomType);

        Task<ActionsResult> Delete(int id);

        Task<IEnumerable<RoomTypeSearchResult>> Search(SearchModel request);
    }
}