using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.HotelServices
{
    public interface IServiceImageRepository
    {
        Task<ActionsResult> Save(UploadServiceImagesRequest serviceImagesRequest);

        Task<IEnumerable<ServiceImage>> GetByServiceId(int id);

        Task<ActionsResult> Delete(int id);
    }
}