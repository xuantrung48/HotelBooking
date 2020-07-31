using HotelBooking.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetAll();
        Task<Promotion> GetByRoomTypeId(int id);
        Task<ActionResult> Save(Promotion promotion);
        Task<ActionResult> Delete(int id);
    }
}
