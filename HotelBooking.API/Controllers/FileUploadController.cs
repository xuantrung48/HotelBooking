using HotelBooking.Domain.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HotelBooking.API.Controllers
{
    [ApiController]
    public class ImageUploadController : Controller
    {
        public readonly IWebHostEnvironment webHostEnvironment;

        public ImageUploadController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionsResult> Post(IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\images\\"))
                    {
                        Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\images\\");
                    }
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    await using (FileStream fileStream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\images\\" + fileName))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        return new ActionsResult()
                        {
                            Id = 1,
                            Message = fileName
                        };
                    }
                }
                catch (Exception e)
                {
                    return new ActionsResult()
                    {
                        Id = 0,
                        Message = e.Message.ToString()
                    };
                }
            }
            else
            {
                return new ActionsResult()
                {
                    Id = 0,
                    Message = "Failed"
                };
            }
        }
    }
}