using HotelBooking.Domain;
using HotelBooking.Domain.Response;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.WEB.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpPost]
        public JsonResult FileUpload([FromBody] FilesUpload filesUpload)
        {
            ActionsResult result = new ActionsResult()
            {
                Id = 0,
                Message = "Error"
            };
            if (filesUpload.Files != null)
            {
                result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/ImageUpload", filesUpload);
            }
            return Json(new { result });
        }
    }
}