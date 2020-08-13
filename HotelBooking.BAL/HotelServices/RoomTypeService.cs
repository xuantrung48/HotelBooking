using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.DAL.Interface.Facilities;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.HotelServices
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository roomTypeRepository;
        private IRoomTypeImageRepository roomTypeImageRepository;
        private IFacilityApplyRepository facilityApplyRepository;
        public RoomTypeService(IRoomTypeRepository roomTypeRepository)
        {
            this.roomTypeRepository = roomTypeRepository;
        }
        public RoomTypeService(IRoomTypeRepository roomTypeRepository, IRoomTypeImageRepository roomTypeImageRepository, IFacilityApplyRepository facilityApplyRepository)
        {
            this.roomTypeRepository = roomTypeRepository;
            this.roomTypeImageRepository = roomTypeImageRepository;
            this.facilityApplyRepository = facilityApplyRepository;
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

        public async Task<ActionsResult> Save(CreateRoomTypeRequest roomType)
        {
            var createRoomtype = new RoomType()
            {
                RoomTypeId = roomType.RoomTypeId,
                DefaultPrice = roomType.DefaultPrice,
                Description = roomType.Description,
                Name = roomType.Name,
                Quantity = roomType.Quantity,
                MaxAdult = roomType.MaxAdult,
                MaxChildren = roomType.MaxChildren,
                MaxPeople = roomType.MaxPeople
            };
            var createRoomtypeResult = await roomTypeRepository.Save(createRoomtype);
            if (createRoomtypeResult.Id != 0)
            {
                _ = await roomTypeImageRepository.Save(new UploadRoomTypeImagesRequest()
                {
                    Images = roomType.Images,
                    RoomTypeId = createRoomtypeResult.Id
                });
                _ = await facilityApplyRepository.Save(new CreateRoomTypeFacilitiesApplyRequest()
                {
                    RoomTypeId = createRoomtypeResult.Id,
                    FacilitieIds = roomType.Facilities
                });
            }
            return createRoomtypeResult;
        }
    }
}