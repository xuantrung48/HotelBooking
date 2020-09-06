using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HotelBooking.Domain
{
    public class FilesUpload
    {
        public IEnumerable<IFormFile> Files { get; set; }
    }
}