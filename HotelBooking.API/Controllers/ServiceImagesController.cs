using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.API.Controllers
{
    [ApiController]
    public class ServiceImagesController : ControllerBase
    {
        private readonly IServiceImageService serviceImageService;

        public ServiceImagesController(IServiceImageService serviceImageService)
        {
            this.serviceImageService = serviceImageService;
        }

        [HttpGet]
        [Route("api/serviceimages/getbyserviceid/{id}")]
        public async Task<IEnumerable<ServiceImage>> GetByServiceId(int id)
        {
            return await serviceImageService.GetByServiceId(id);
        }

        [HttpPost]
        [Route("api/serviceimages/save")]
        public async Task<ActionsResult> Save(UploadServiceImagesRequest service)
        {
            return await serviceImageService.Save(service);
        }

        [HttpDelete]
        [Route("api/serviceimages/delete/{id}")]
        public async Task<ActionsResult> Remove(int id)
        {
            return await serviceImageService.Delete(id);
        }
    }
}