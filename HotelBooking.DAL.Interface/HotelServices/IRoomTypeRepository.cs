using HotelBooking.Domain.Request.Search;
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

        Task<ActionsResult> Save(RoomType roomType);

        Task<ActionsResult> Delete(int id);

        Task<IEnumerable<RoomTypeSearchResult>> Search(SearchModel request);
    }
}