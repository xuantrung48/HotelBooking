using HotelBooking.Domain;
using HotelBooking.Domain.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.API.Controllers
{
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        public readonly IWebHostEnvironment webHostEnvironment;

        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<List<ActionsResult>> Post([FromForm] FilesUpload filesUpload)
        {
            var result = new List<ActionsResult>();
            if (filesUpload.Files.Count() > 0)
            {
                try
                {
                    if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\images\\"))
                    {
                        Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\images\\");
                    }
                    foreach (var file in filesUpload.Files)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        await using (FileStream fileStream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\images\\" + fileName))
                        {
                            file.CopyTo(fileStream);
                            fileStream.Flush();
                            result.Add(new ActionsResult()
                            {
                                Id = 1,
                                Message = fileName
                            });
                        }
                    }
                }
                catch (Exception e)
                {
                    result.Add(new ActionsResult()
                    {
                        Id = 0,
                        Message = e.Message.ToString()
                    });
                }
            }
            else
            {
                result.Add(new ActionsResult()
                {
                    Id = 0,
                    Message = "Error"
                });
            }
            return result;
        }
    }
}