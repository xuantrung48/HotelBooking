using HotelBooking.Domain.User;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Domain.Response.Account
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nhập vào mật khẩu!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp!")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public string Avatar { get; set; }

        [Required(ErrorMessage = "Nhập vào tên!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Độ dài của tên trong khoảng từ 5 đến 50 ký tự!")]
        public string Name { get; set; }

        [RegularExpression(@"^\(?(0|[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }
    }
}