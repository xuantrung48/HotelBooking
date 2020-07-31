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
        public async Task<RoomType> Get(int id)
        {
            return await roomTypeRepository.Get(id);
        }

        public async Task<IEnumerable<RoomType>> Get()
        {
            return await roomTypeRepository.Get();
        }

        public async Task<ActionResult> Delete(int id)
        {
            return await roomTypeRepository.Delete(id);
        }

        public async Task<ActionResult> Save(RoomType roomType)
        {
            return await roomTypeRepository.Save(roomType);
        }
    }
}