using HotelBooking.BAL.Interface;
using HotelBooking.DAL.Interface;
using HotelBooking.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL
{
    public class CustomerSerice : ICustomerSevice
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerSerice(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Customer> Get(int id)
        {
            return await customerRepository.Get(id);
        }

        public async Task<IEnumerable<Customer>> Get()
        {
            return await customerRepository.Get();
        }

        public async Task<ActionsResult> Delete(int id)
        {
            return await customerRepository.Delete(id);
        }

        public async Task<ActionsResult> Save(Customer customer)
        {
            return await customerRepository.Save(customer);
        }
    }
}