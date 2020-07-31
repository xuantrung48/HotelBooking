using HotelBooking.BAL.Interface;
using HotelBooking.DAL.Interface;
using HotelBooking.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<Promotion> GetByRoomTypeId(int id)
        {
            return await promotionRepository.GetByRoomTypeId(id);
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
