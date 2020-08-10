using HotelBooking.Domain.Request.Account;
using HotelBooking.Domain.Response.Account;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Web.Ultilities;

namespace HotelBooking.WEB.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var loginRequest = new LoginRequest()
                {
                    Email = model.Email,
                    Password = model.Password
                };
                LoginResult result = ApiHelper<LoginResult>.HttpPostAsync($"{Helper.ApiUrl}api/account/login", loginRequest);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", result.Message);
                return View();
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var registerRequest = new RegisterRequest()
                {
                    Email = model.Email,
                    Password = model.Password,
                    Gender = model.Gender,
                    /*ImageFile = model.ImageFile,*/
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber
                    
                };
                RegisterResult result = ApiHelper<RegisterResult>.HttpPostAsync($"{Helper.ApiUrl}api/account/register", registerRequest);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", result.Message);
                return View();
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            var result = true;
            return Json(new { result });
        }
    }
}
