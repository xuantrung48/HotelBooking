using HotelBooking.BAL.Interface;
using HotelBooking.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.API.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerSevice customerSevice;

        public CustomersController(ICustomerSevice customerSevice)
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
        public async Task<ActionsResult> Save(Customer customer)
        {
            return await customerSevice.Save(customer);
        }

        [HttpDelete]
        [Route("api/customer/delete/{id}")]
        public async Task<ActionsResult> Remove(int id)
        {
            return await customerSevice.Delete(id);
        }
    }
}