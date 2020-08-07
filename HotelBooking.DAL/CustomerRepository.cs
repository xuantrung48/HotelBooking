using Dapper;
using HotelBooking.DAL.Interface;
using HotelBooking.Domain.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HotelBooking.DAL
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public async Task<Customer> Get(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<Customer>(cnn: conn, sql: "Customer_GetByCustomerId", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Customer>> Get()
        {
            return await SqlMapper.QueryAsync<Customer>(conn, "Customer_GetAll", commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", id);
            return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "Customer_Delete", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<ActionsResult> Save(Customer customer)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customer.CustomerId);
                parameters.Add("@Name", customer.Name);
                parameters.Add("@PhoneNumber", customer.PhoneNumber);
                parameters.Add("@Email", customer.Email);
                return await SqlMapper.QueryFirstOrDefaultAsync<ActionsResult>(cnn: conn, sql: "Customer_Save", param: parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                return new ActionsResult()
                {
                    Id = 0,
                    Message = "Có lỗi xảy ra, xin thử lại!"
                };
            }
        }
    }
}