using HotelBooking.BAL.Interface.Promotions;
using HotelBooking.DAL.Interface.Promotions;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Promotions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository promotionRepository;
        public PromotionService(IPromotionRepository promotionRepository)
        {
            this.promotionRepository = promotionRepository;
        }

        public async Task<IEnumerable<Promotion>> GetAll()
        {
            return await promotionRepository.GetAll();
        }

        public async Task<ActionResult> Delete(int id)
        {
            return await promotionRepository.Delete(id);
        }

        public async Task<ActionResult> Save(Promotion promotion)
        {
            return await promotionRepository.Save(promotion);
        }
    }
}
