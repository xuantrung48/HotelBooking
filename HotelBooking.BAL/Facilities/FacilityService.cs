using HotelBooking.BAL.Interface.Facilities;
using HotelBooking.DAL.Interface.Facilities;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Facilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Facilities
{
    public class FacilityService : IFacilityService
    {
        private readonly IFacilityRepository facilityRepository;

        public FacilityService(IFacilityRepository facilityRepository)
        {
            this.facilityRepository = facilityRepository;
        }

        public async Task<Facility> GetById(int id)
        {
            return await facilityRepository.GetById(id);
        }

        public async Task<IEnumerable<Facility>> GetAll()
        {
            return await facilityRepository.GetAll();
        }

        public async Task<ActionsResult> Delete(int id)
        {
            return await facilityRepository.Delete(id);
        }

        public async Task<ActionsResult> Save(Facility facility)
        {
            return await facilityRepository.Save(facility);
        }
    }
}