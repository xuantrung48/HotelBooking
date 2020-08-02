using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.HotelServices
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> Get();
        Task<Service> Get(int id);
        Task<ActionResult> Save(Service service);
        Task<ActionResult> Delete(int id);
        Task<IEnumerable<Service>> Search(string keyWord);
    }
}
