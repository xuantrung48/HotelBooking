using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Promotions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.Promotions
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetAll();
        Task<ActionResult> Save(Promotion promotion);
        Task<ActionResult> Delete(int id);
    }
}
