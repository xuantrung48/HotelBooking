using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.API.Controllers
{
    [ApiController]
    public class RoomTypeImagesController : ControllerBase
    {
        private readonly IRoomTypeImageService roomTypeImageService;

        public RoomTypeImagesController(IRoomTypeImageService roomTypeImageService)
        {
            this.roomTypeImageService = roomTypeImageService;
        }

        [HttpGet]
        [Route("api/roomtypeimages/getbyroomtypeid/{id}")]
        public async Task<IEnumerable<RoomTypeImage>> GetByRoomTypeId(int id)
        {
            return await roomTypeImageService.GetByRoomTypeId(id);
        }

        [HttpPost]
        [Route("api/roomtypeimages/save")]
        public async Task<ActionsResult> Save(UploadRoomTypeImagesRequest roomType)
        {
            return await roomTypeImageService.Save(roomType);
        }

        [HttpDelete]
        [Route("api/roomtypeimages/delete/{id}")]
        public async Task<ActionsResult> Remove(int id)
        {
            return await roomTypeImageService.Delete(id);
        }
    }
}