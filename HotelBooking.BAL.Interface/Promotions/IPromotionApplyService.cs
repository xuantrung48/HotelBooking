using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Promotions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.Promotions
{
    public interface IPromotionApplyService
    {
        Task<IEnumerable<PromotionApply>> GetAll();

        Task<IEnumerable<PromotionApply>> GetByRoomTypeId(int id);

        Task<IEnumerable<PromotionApply>> GetByPromotionId(int id);

        Task<ActionsResult> Save(PromotionApply promotionApply);

        Task<ActionsResult> Delete(int id);

        Task<ActionsResult> DeleteByPromotionId(int id);
    }
}