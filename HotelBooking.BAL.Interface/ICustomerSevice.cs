using HotelBooking.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface
{
    public interface ICustomerSevice
    {
        Task<IEnumerable<Customer>> Get();
        Task<Customer> Get(int id);
        Task<ActionResult> Save(Customer customer);
        Task<ActionResult> Delete(int id);
    }
}
