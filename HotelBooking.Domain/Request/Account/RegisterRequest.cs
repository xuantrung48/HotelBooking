using HotelBooking.Domain.Response.Account;
using Microsoft.AspNetCore.Http;

namespace HotelBooking.Domain.Request.Account
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        /*public IFormFile ImageFile { get; set; }*/
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
    }
}
