using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.DAL.Interface.Facilities;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Facilities;
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
        private IFacilityRepository facilityRepository;
        public RoomTypeService(IRoomTypeRepository roomTypeRepository)
        {
            this.roomTypeRepository = roomTypeRepository;
        }
        public RoomTypeService(IFacilityRepository facilityRepository, IRoomTypeRepository roomTypeRepository, IRoomTypeImageRepository roomTypeImageRepository, IFacilityApplyRepository facilityApplyRepository)
        {
            this.roomTypeRepository = roomTypeRepository;
            this.roomTypeImageRepository = roomTypeImageRepository;
            this.facilityApplyRepository = facilityApplyRepository;
            this.facilityRepository = facilityRepository;
        }

        public async Task<RoomType> GetById(int id)
        {
            return await roomTypeRepository.GetById(id);
        }
        public async Task<RoomType> GetByIdWithImagesAndFacilities(int id)
        {
            var roomType = await roomTypeRepository.GetById(id);
            roomType.Images = await roomTypeImageRepository.GetByRoomTypeId(id);
            var facilitiesApply = await facilityApplyRepository.GetByRoomTypeId(id);
            var facilities = new List<Facility>();
            foreach (var facility in facilitiesApply)
                facilities.Add(await facilityRepository.GetById(facility.FacilityId));
            roomType.Facilities = facilities;
            return roomType;
        }

        public async Task<IEnumerable<RoomType>> GetAll()
        {
            return await roomTypeRepository.GetAll();
        }
        public async Task<IEnumerable<RoomType>> GetAllRoomTypeWithImagesAndFacilities()
        {
            var roomTypes = await roomTypeRepository.GetAll();
            foreach(var roomType in roomTypes)
            {
                roomType.Images = await roomTypeImageRepository.GetByRoomTypeId(roomType.RoomTypeId);
                var facilitiesApply = await facilityApplyRepository.GetByRoomTypeId(roomType.RoomTypeId);
                var facilities = new List<Facility>();
                foreach (var facility in facilitiesApply)
                    facilities.Add(await facilityRepository.GetById(facility.FacilityId));
                roomType.Facilities = facilities;
            }
            return roomTypes;
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