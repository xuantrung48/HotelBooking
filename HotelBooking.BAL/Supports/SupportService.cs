using HotelBooking.BAL.Interface.Supports;
using HotelBooking.DAL.Interface.Supports;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Supports
{
    public class SupportService : ISupportService
    {
        private readonly ISupportRepository supportRepository;

        public SupportService(ISupportRepository supportRepository)
        {
            this.supportRepository = supportRepository;
        }

        public Task<IEnumerable<DateTime>> CreateTableDateAsync(DateTime startDate, DateTime endDate)
        {
            return supportRepository.CreateTableDateAsync(startDate, endDate);
        }
    }
}