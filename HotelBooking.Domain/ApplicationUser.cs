using Microsoft.AspNetCore.Identity;

namespace HotelBooking.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Avatar { get; set; }
    }
}
