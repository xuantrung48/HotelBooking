using HotelBooking.Domain.Request.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.HotelServices
{
    public interface IServiceImageService
    {
        Task<IEnumerable<ServiceImage>> GetByServiceId(int id);

        Task<ActionsResult> Save(UploadServiceImagesRequest serviceImage);

        Task<ActionsResult> Delete(int id);
    }
}