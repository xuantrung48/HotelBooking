using HotelBooking.BAL.Interface;
using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.HotelServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActionResult = HotelBooking.Domain.Response.ActionResult;

namespace HotelBooking.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerSevice customerSevice;
        public CustomerController(ICustomerSevice customerSevice)
        {
            this.customerSevice = customerSevice;
        }

        [HttpGet]
        [Route("api/customer/get")]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await customerSevice.Get();
        }

        [HttpGet]
        [Route("api/customer/get/{id}")]
        public async Task<Customer> Get(int id)
        {
            return await customerSevice.Get(id);
        }  
        
        [HttpPost]
        [Route("api/customer/save")]
        public async Task<ActionResult> Save(Customer customer)
        {
            return await customerSevice.Save(customer);
        }

        [HttpDelete]
        [Route("api/customer/delete/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            return await customerSevice.Delete(id);
        }
    }
}
