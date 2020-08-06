﻿using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Facilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.Facilities
{
    public interface IFacilityApplyRepository
    {
        Task<IEnumerable<FacilityApply>> GetByRoomTypeId(int id);
        Task<ActionsResult> Save(FacilityApply facilityApply);
        Task<ActionsResult> Delete(int id);
        Task<ActionsResult> DeleteByRoomTypeId(int id);
    }
}
