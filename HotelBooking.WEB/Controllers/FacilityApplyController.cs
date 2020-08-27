using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Facilities;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelBooking.WEB.Controllers
{
    public class FacilityApplyController : Controller
    {
        public JsonResult Get(int id)
        {
            List<FacilityApply> result = ApiHelper<List<FacilityApply>>.HttpGetAsync($"{Helper.ApiUrl}api/facilityapply/getbyroomtypeid/{id}");
            return Json(new { result });
        }

        public JsonResult Delete(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/facilityapply/delete/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult DeleteByRoomTypeId(int id)
        {
            ActionsResult result = ApiHelper<ActionsResult>.HttpGetAsync($"{Helper.ApiUrl}api/facilityapply/deletebyroomtypeid/{id}", "DELETE");
            return Json(new { result });
        }

        public JsonResult Save([FromBody] FacilityApply model)
        {
            ActionsResult result;
            result = ApiHelper<ActionsResult>.HttpPostAsync($"{Helper.ApiUrl}api/facilityapply/save", model);
            return Json(new { result });
        }
    }
}