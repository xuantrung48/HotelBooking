﻿using HotelBooking.BAL.Interface.Promotions;
using HotelBooking.DAL.Interface.Promotions;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Promotions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Promotions
{
    public class PromotionApplyService : IPromotionApplyService
    {
        private readonly IPromotionApplyRepository promotionApplyRepository;
        public PromotionApplyService(IPromotionApplyRepository promotionApplyRepository)
        {
            this.promotionApplyRepository = promotionApplyRepository;
        }

        public async Task<IEnumerable<PromotionApply>> GetAll()
        {
            return await promotionApplyRepository.GetAll();
        }

        public async Task<ActionResult> Delete(int id)
        {
            return await promotionApplyRepository.Delete(id);
        }

        public async Task<ActionResult> Save(PromotionApply promotionApply)
        {
            return await promotionApplyRepository.Save(promotionApply);
        }

        public async Task<PromotionApply> GetByRoomTypeId(int id)
        {
            return await promotionApplyRepository.GetByRoomTypeId(id);
        }
    }
}
