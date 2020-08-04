using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Promotions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.Promotions
{
    public interface IPromotionRepository
    {
        Task<IEnumerable<Promotion>> GetAll();
        Task<ActionResult> Save(Promotion promotion);
        Task<Promotion> GetById(int id);
        Task<ActionResult> Delete(int id);
    }
}
