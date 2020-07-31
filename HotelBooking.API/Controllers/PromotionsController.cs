using HotelBooking.BAL.Interface;
using HotelBooking.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionResult = HotelBooking.Domain.Response.ActionResult;

namespace HotelBooking.API.Controllers
{
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionService promotionService;
        public PromotionsController(IPromotionService promotionService)
        {
            this.promotionService = promotionService;
        }

        [HttpGet]
        [Route("api/promotions/getall")]
        public async Task<IEnumerable<Promotion>> GetAll()
        {
            return await promotionService.GetAll();
        }
        [HttpGet]
        [Route("api/promotions/getbyroomtypeid/{id}")]
        public async Task<Promotion> GetById(int id)
        {
            return await promotionService.GetByRoomTypeId(id);
        }
        [HttpPost]
        [Route("api/promotions/save")]
        public async Task<ActionResult> Save(Promotion promotion)
        {
            return await promotionService.Save(promotion);
        }

        [HttpDelete]
        [Route("api/promotions/delete/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            return await promotionService.Delete(id);
        }
    }
}
