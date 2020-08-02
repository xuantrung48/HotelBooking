﻿using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Promotions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.Promotions
{
    public interface IPromotionApplyRepository
    {
        Task<IEnumerable<PromotionApply>> GetAll();
        Task<PromotionApply> GetByRoomTypeId(int id);
        Task<ActionResult> Save(PromotionApply promotionApply);
        Task<ActionResult> Delete(int id);
    }
}
