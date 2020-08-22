using HotelBooking.Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Web.Ultilities;

namespace HotelBooking.WEB.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpPost]
        public JsonResult ImageUpload(IFormFile file)
        {
            ActionsResult result = new ActionsResult()
            {
                Id = 0,
                Message = "Error"
            };
            if (file != null)
            {
                result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/ImageUpload", file);
            }
            return Json(new { result });
        }
    }
}