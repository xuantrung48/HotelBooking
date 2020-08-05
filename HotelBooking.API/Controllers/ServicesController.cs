using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.Domain.Response.HotelServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionsResult = HotelBooking.Domain.Response.ActionsResult;

namespace HotelBooking.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService serviceService;
        public ServicesController(IServiceService serviceService)
        {
            this.serviceService = serviceService;
        }

        [HttpGet]
        [Route("api/service/get")]
        public async Task<IEnumerable<Service>> Get()
        {
            return await serviceService.Get();
        }

        [HttpGet]
        [Route("api/service/get/{id}")]
        public async Task<Service> Get(int id)
        {
            return await serviceService.Get(id);
        }  
        
        [HttpPost]
        [Route("api/service/save")]
        public async Task<ActionsResult> Save([FromBody] Service service)
        {
            return await serviceService.Save(service);
        }

        [HttpDelete]
        [Route("api/service/delete/{id}")]
        public async Task<ActionsResult> Remove(int id)
        {
            return await serviceService.Delete(id);
        }
        [HttpGet]
        [Route("api/service/search")]
        public async Task<IEnumerable<Service>> Search(string keyWord)
        {
            return await serviceService.Search(keyWord);
        }
    }
}
