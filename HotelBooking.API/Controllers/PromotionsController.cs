using HotelBooking.BAL.Interface.Promotions;
using HotelBooking.Domain.Response.Promotions;
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
        [Route("api/promotions/getbyid/{id}")]
        public async Task<Promotion> GetById(int id)
        {
            return await promotionService.GetById(id);
        }
        [HttpGet]
        [Route("api/promotions/getall")]
        public async Task<IEnumerable<Promotion>> GetAll()
        {
            return await promotionService.GetAll();
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
