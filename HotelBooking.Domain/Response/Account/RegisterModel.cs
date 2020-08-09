using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Domain.Response.Account
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp!")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
