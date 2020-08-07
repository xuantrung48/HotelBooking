using HotelBooking.BAL.Interface.Facilities;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Facilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.API.Controllers
{
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly IFacilityService facilityService;

        public FacilitiesController(IFacilityService facilityService)
        {
            this.facilityService = facilityService;
        }

        [HttpGet]
        [Route("api/facilities/getall")]
        public async Task<IEnumerable<Facility>> GetAll()
        {
            return await facilityService.GetAll();
        }

        [HttpGet]
        [Route("api/facilities/getbyid/{id}")]
        public async Task<Facility> GetById(int id)
        {
            return await facilityService.GetById(id);
        }

        [HttpPost]
        [Route("api/facilities/save")]
        public async Task<ActionsResult> Save(Facility facility)
        {
            return await facilityService.Save(facility);
        }

        [HttpDelete]
        [Route("api/facilities/delete/{id}")]
        public async Task<ActionsResult> Remove(int id)
        {
            return await facilityService.Delete(id);
        }
    }
}