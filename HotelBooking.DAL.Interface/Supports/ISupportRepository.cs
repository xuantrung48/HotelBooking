using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Interface.Supports
{
    public interface ISupportRepository
    {
        Task<IEnumerable<DateTime>> CreateTableDateAsync(DateTime startDate, DateTime endDate);
    }
}