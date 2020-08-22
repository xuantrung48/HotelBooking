using HotelBooking.BAL.Interface.Supports;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.API.Controllers
{
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly ISupportService supportService;

        public SupportController(ISupportService supportService)
        {
            this.supportService = supportService;
        }

        // bắt đầu nể nể rồi ddosoo là rg =.=
        [HttpGet]
        [Route("api/support/CreateDateTable")]
        public async Task<IEnumerable<DateTime>> Get(DateTime startDate, DateTime endDate)
        {
            return await supportService.CreateTableDateAsync(startDate, endDate);
        }
    }
}