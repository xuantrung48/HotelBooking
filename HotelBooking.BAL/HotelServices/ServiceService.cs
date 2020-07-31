using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.HotelServices
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;
        public ServiceService(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }
        public async Task<Service> Get(int id)
        {
            return await serviceRepository.Get(id);
        }

        public async Task<IEnumerable<Service>> Get()
        {
            return await serviceRepository.Get();
        }

        public async Task<ActionResult> Delete(int id)
        {
            return await serviceRepository.Delete(id);
        }

        public async Task<ActionResult> Save(Service service)
        {
            return await serviceRepository.Save(service);
        }
    }
}
