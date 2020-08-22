using HotelBooking.BAL.Interface.Promotions;
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

        public async Task<ActionsResult> Delete(int id)
        {
            return await promotionApplyRepository.Delete(id);
        }

        public async Task<ActionsResult> Save(PromotionApply promotionApply)
        {
            return await promotionApplyRepository.Save(promotionApply);
        }

        public async Task<IEnumerable<PromotionApply>> GetByRoomTypeId(int id)
        {
            return await promotionApplyRepository.GetByRoomTypeId(id);
        }

        public async Task<IEnumerable<PromotionApply>> GetByPromotionId(int id)
        {
            return await promotionApplyRepository.GetByPromotionId(id);
        }

        public async Task<ActionsResult> DeleteByPromotionId(int id)
        {
            return await promotionApplyRepository.DeleteByPromotionId(id);
        }
    }
}