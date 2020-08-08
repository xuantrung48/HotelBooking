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
    public class RoomTypesController : ControllerBase
    {
        private readonly IRoomTypeService roomTypeService;

        public RoomTypesController(IRoomTypeService roomTypeService)
        {
            this.roomTypeService = roomTypeService;
        }

        [HttpGet]
        [Route("api/roomtypes/getall")]
        public async Task<IEnumerable<RoomType>> GetAll()
        {
            return await roomTypeService.GetAll();
        }

        [HttpGet]
        [Route("api/roomtypes/getbyid/{id}")]
        public async Task<RoomType> GetById(int id)
        {
            return await roomTypeService.GetById(id);
        }

        [HttpPost]
        [Route("api/roomtypes/save")]
        public async Task<ActionsResult> Save(CreateRoomTypeRequest roomType)
        {
            return await roomTypeService.Save(roomType);
        }

        [HttpDelete]
        [Route("api/roomtypes/delete/{id}")]
        public async Task<ActionsResult> Remove(int id)
        {
            return await roomTypeService.Delete(id);
        }
    }
}