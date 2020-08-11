using Microsoft.AspNetCore.Identity;

namespace HotelBooking.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
}
