﻿using HotelBooking.BAL.Interface.Promotions;
using HotelBooking.Domain.Response.Promotions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionsResult = HotelBooking.Domain.Response.ActionsResult;

namespace HotelBooking.API.Controllers
{
    public class PromotionApplyController : ControllerBase
    {
        private readonly IPromotionApplyService promotionApplyService;
        public PromotionApplyController(IPromotionApplyService promotionApplyService)
        {
            this.promotionApplyService = promotionApplyService;
        }

        [HttpGet]
        [Route("api/promotionapply/getall")]
        public async Task<IEnumerable<PromotionApply>> GetAll()
        {
            return await promotionApplyService.GetAll();
        }
        [HttpPost]
        [Route("api/promotionapply/save")]
        public async Task<ActionsResult> Save(PromotionApply promotionApply)
        {
            return await promotionApplyService.Save(promotionApply);
        }

        [HttpDelete]
        [Route("api/promotionapply/delete/{id}")]
        public async Task<ActionsResult> Remove(int id)
        {
            return await promotionApplyService.Delete(id);
        }
        [HttpGet]
        [Route("api/promotionapply/getbyroomtypeid/{id}")]
        public async Task<PromotionApply> GetByRoomTypeId(int id)
        {
            return await promotionApplyService.GetByRoomTypeId(id);
        }
    }
}
