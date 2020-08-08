using HotelBooking.BAL.Interface.Facilities;
using HotelBooking.DAL.Interface.Facilities;
using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Facilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Facilities
{
    public class FacilityApplyService : IFacilityApplyService
    {
        private readonly IFacilityApplyRepository facilityApplyRepository;

        public FacilityApplyService(IFacilityApplyRepository facilityApplyRepository)
        {
            this.facilityApplyRepository = facilityApplyRepository;
        }

        public async Task<ActionsResult> Delete(int id)
        {
            return await facilityApplyRepository.Delete(id);
        }

        public async Task<ActionsResult> Save(CreateRoomTypeFacilitiesApplyRequest facilityApply)
        {
            return await facilityApplyRepository.Save(facilityApply);
        }

        public async Task<IEnumerable<FacilityApply>> GetByRoomTypeId(int id)
        {
            return await facilityApplyRepository.GetByRoomTypeId(id);
        }

        public async Task<ActionsResult> DeleteByRoomTypeId(int id)
        {
            return await facilityApplyRepository.DeleteByRoomTypeId(id);
        }
    }
}