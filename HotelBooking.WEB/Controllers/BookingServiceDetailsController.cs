using HotelBooking.Domain.Response;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelBooking.WEB.Controllers
{
    public class BookingServiceDetails : Controller
    {
        public JsonResult Get(int id)
        {
            List<BookingServiceDetails> result = ApiHelper<List<BookingServiceDetails>>.HttpGetAsync($"{Helper.ApiUrl}api/bookingServiceDetails/get/{id}");
            return Json(new { result });
        }

        public JsonResult Delete(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/bookingServiceDetails/delete/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult DeleteByBookingId(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/bookingServiceDetails/deletebyBookingId/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult Save([FromBody] BookingServiceDetails model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/bookingServiceDetails/save", model);
            return Json(new { result });
        }
    }
}