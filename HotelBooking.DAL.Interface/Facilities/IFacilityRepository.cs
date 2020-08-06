using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Facilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.Facilities
{
    public interface IFacilityRepository
    {
        Task<IEnumerable<Facility>> GetAll();

        Task<Facility> GetById(int id);

        Task<ActionsResult> Save(Facility facility);

        Task<ActionsResult> Delete(int id);
    }
}