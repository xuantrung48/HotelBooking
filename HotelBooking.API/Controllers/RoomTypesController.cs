using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.Domain.Response.HotelServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionResult = HotelBooking.Domain.Response.ActionResult;

namespace HotelBooking.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class RoomTypesController : ControllerBase
    {
        private readonly IRoomTypeService roomTypeService;
        public RoomTypesController(IRoomTypeService roomTypeService)
        {
            this.roomTypeService = roomTypeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/roomtypes/get")]
        public async Task<IEnumerable<RoomType>> Get()
        {
            return await roomTypeService.Get();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/roomtypes/get/{id}")]
        public async Task<RoomType> Get(int id)
        {
            return await roomTypeService.Get(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomType"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/roomtypes/save")]
        public async Task<ActionResult> Save(RoomType roomType)
        {
            return await roomTypeService.Save(roomType);
        }
        [HttpDelete]
        [Route("api/roomtypes/delete/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            return await roomTypeService.Delete(id);
        }
    }
}
