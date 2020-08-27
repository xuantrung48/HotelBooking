using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.DAL.Interface.Facilities;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Request.Search;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Facilities;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.BAL.HotelServices
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository roomTypeRepository;
        private IRoomTypeImageRepository roomTypeImageRepository;
        private IFacilityApplyRepository facilityApplyRepository;
        private IFacilityRepository facilityRepository;

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

        public async Task<IEnumerable<RoomTypes>> GetAllRoomTypeWithImages()
        {
            var getRoomTypes = new List<RoomTypes>();
            var roomTypes = await roomTypeRepository.GetAll();
            foreach (var roomType in roomTypes)
            {
                getRoomTypes.Add(new RoomTypes()
                {
                    MaxAdult = roomType.MaxAdult,
                    DefaultPrice = roomType.DefaultPrice,
                    Description = roomType.Description,
                    MaxChildren = roomType.MaxChildren,
                    MaxPeople = roomType.MaxPeople,
                    Name = roomType.Name,
                    Quantity = roomType.Quantity,
                    RoomTypeId = roomType.RoomTypeId,
                    Image = (await roomTypeImageRepository.GetByRoomTypeId(roomType.RoomTypeId)).FirstOrDefault().ImageData
                });
            }
            return getRoomTypes;
        }

        public async Task<ActionsResult> Delete(int id)
        {
            return await roomTypeRepository.Delete(id);
        }

        public async Task<IEnumerable<RoomTypes>> GetAllRoomTypeWithImagesAndFacilities()
        {
            var getRoomTypes = new List<RoomTypes>();
            var roomTypes = await roomTypeRepository.GetAll();
            foreach (var roomType in roomTypes)
            {
                var facilitiesApply = await facilityApplyRepository.GetByRoomTypeId(roomType.RoomTypeId);
                var facilities = new List<Facility>();
                foreach (var facility in facilitiesApply)
                    facilities.Add(await facilityRepository.GetById(facility.FacilityId));
                getRoomTypes.Add(new RoomTypes()
                {
                    MaxAdult = roomType.MaxAdult,
                    DefaultPrice = roomType.DefaultPrice,
                    Description = roomType.Description,
                    MaxChildren = roomType.MaxChildren,
                    MaxPeople = roomType.MaxPeople,
                    Name = roomType.Name,
                    Quantity = roomType.Quantity,
                    RoomTypeId = roomType.RoomTypeId,
                    Image = (await roomTypeImageRepository.GetByRoomTypeId(roomType.RoomTypeId)).FirstOrDefault().ImageData,
                    Facilities = facilities
                });
            }
            return getRoomTypes;
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

        public async Task<IEnumerable<RoomTypeSearchResult>> Search(SearchModel request)
        {
            return await roomTypeRepository.Search(request);
        }
    }
}