﻿using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Promotions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.Promotions
{
    public interface IPromotionApplyService
    {
        Task<IEnumerable<PromotionApply>> GetAll();
        Task<PromotionApply> GetByRoomTypeId(int id);
        Task<ActionResult> Save(PromotionApply promotionApply);
        Task<ActionResult> Delete(int id);
    }
}