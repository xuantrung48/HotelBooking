using HotelBooking.BAL.Interface.Promotions;
using HotelBooking.Domain.Request.Booking;
using HotelBooking.Domain.Request.Promotions;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Promotions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.API.Controllers
{
    [ApiController]
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
        public async Task<ActionsResult> Save(SavePromotionRequest promotion)
        {
            return await promotionService.Save(promotion);
        }

        [HttpDelete]
        [Route("api/promotions/delete/{id}")]
        public async Task<ActionsResult> Remove(int id)
        {
            return await promotionService.Delete(id);
        }

        [HttpGet]
        [Route("api/promotions/getavailable")]
        public async Task<IEnumerable<GetMaxDiscountRatesPromotionAvailable>> GetAvailable()
        {
            return await promotionService.GetAvailable();
        }
        [HttpPost]
        [Route("api/promotions/getavailablefordate")]
        public async Task<IEnumerable<GetMaxDiscountRatesPromotionAvailable>> GetAvailableForDate([FromBody] DateTime date)
        {
            return await promotionService.GetAvailableForDate(date);
        }
        [HttpPost]
        [Route("api/promotions/getavailablefordateandroomtypeid")]
        public async Task<GetAvailablePromotionForDateAndRoomIdResponse> GetAvailablePromotionForDateAndRoomId([FromBody] GetAvailablePromotionForDateAndRoomIdRequest request)
        {
            return await promotionService.GetAvailablePromotionForDateAndRoomId(request);
        }
    }
}