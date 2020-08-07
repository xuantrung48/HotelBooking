using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.HotelServices
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository roomTypeRepository;

        public RoomTypeService(IRoomTypeRepository roomTypeRepository)
        {
            this.roomTypeRepository = roomTypeRepository;
        }

        public async Task<RoomType> GetById(int id)
        {
            return await roomTypeRepository.GetById(id);
        }

        public async Task<IEnumerable<RoomType>> GetAll()
        {
            return await roomTypeRepository.GetAll();
        }

        public async Task<ActionsResult> Delete(int id)
        {
            return await roomTypeRepository.Delete(id);
        }

        public async Task<ActionsResult> Save(RoomType roomType)
        {
            return await roomTypeRepository.Save(roomType);
        }
    }
}