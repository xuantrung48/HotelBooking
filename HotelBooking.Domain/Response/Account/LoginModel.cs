using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Domain.Response.Account
{
    public class LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}