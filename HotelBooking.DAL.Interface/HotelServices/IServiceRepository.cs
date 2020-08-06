using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.HotelServices
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> Get();

        Task<Service> Get(int id);

        Task<ActionsResult> Save(Service service);

        Task<ActionsResult> Delete(int id);

        Task<IEnumerable<Service>> Search(string keyWord);
    }
}