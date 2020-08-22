using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.BAL.HotelServices
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceImageRepository serviceImageRepository;

        public ServiceService(IServiceRepository serviceRepository, IServiceImageRepository serviceImageRepository)
        {
            this.serviceRepository = serviceRepository;
            this.serviceImageRepository = serviceImageRepository;
        }

        public async Task<Service> Get(int id)
        {
            return await serviceRepository.Get(id);
        }

        public async Task<Service> GetByIdWithImages(int id)
        {
            var service = await serviceRepository.Get(id);
            service.Images = await serviceImageRepository.GetByServiceId(id);
            return service;
        }

        public async Task<IEnumerable<Service>> Get()
        {
            return await serviceRepository.Get();
        }

        public async Task<ActionsResult> Delete(int id)
        {
            return await serviceRepository.Delete(id);
        }

        public async Task<ActionsResult> Save(CreateServiceRequest request)
        {
            var service = new Service()
            {
                Description = request.Description,
                Price = request.Price,
                ServiceId = request.ServiceId,
                ServiceName = request.ServiceName,
                IsDelete = request.IsDelete
            };
            var createServiceResult = await serviceRepository.Save(service);
            if (createServiceResult.Id != 0)
            {
                _ = serviceImageRepository.Save(new UploadServiceImagesRequest()
                {
                    ServiceId = createServiceResult.Id,
                    Images = request.Images
                });
            }
            return createServiceResult;
        }

        public async Task<IEnumerable<Service>> Search(string keyWord)
        {
            return await serviceRepository.Search(keyWord);
        }

        public async Task<IEnumerable<Services>> GetAllWithImages()
        {
            var services = await serviceRepository.Get();
            var servicesResult = new List<Services>();
            foreach (var service in services)
            {
                servicesResult.Add(new Services()
                {
                    Description = service.Description,
                    Price = service.Price,
                    IsDelete = service.IsDelete,
                    ServiceId = service.ServiceId,
                    ServiceName = service.ServiceName,
                    Image = (await serviceImageRepository.GetByServiceId(service.ServiceId)).FirstOrDefault().ImageData
                });
            }
            return servicesResult;
        }
    }
}