using HotelBooking.BAL.HotelServices;
using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.DAL.Facilities;
using HotelBooking.DAL.HotelServices;
using HotelBooking.DAL.Interface.Facilities;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.Domain.Request.HotelServices;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HotelBooking.Test
{
    public class RoomTypeTest
    {
        private IRoomTypeService roomTypeService;
        private IRoomTypeRepository roomTypeRepository = new RoomTypeRepository();
        private IRoomTypeImageRepository roomTypeImageRepository = new RoomTypeImageRepository();
        private IFacilityApplyRepository facilityApplyRepository = new FacilityApplyRepository();
        private IFacilityRepository facilityRepository = new FacilityRepository();

        [SetUp]
        public void Setup()
        {
            roomTypeService = new RoomTypeService(facilityRepository, roomTypeRepository, roomTypeImageRepository, facilityApplyRepository);
        }

        [Test]
        public void CreateRoomType_Success()
        {
            var request = new CreateRoomTypeRequest()
            {
                RoomTypeId = 0,
                DefaultPrice = 300000,
                Description = "Description",
                Name = "Normal",
                Quantity = 1,
                MaxAdult = 2,
                MaxChildren = 1,
                MaxPeople = 4
            };
            var result = Task.Run(async () => await roomTypeService.Save(request)).Result;
            Assert.IsTrue(result.Id != 0);
        }
    }
}