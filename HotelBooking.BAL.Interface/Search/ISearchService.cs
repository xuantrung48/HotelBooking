using HotelBooking.Domain.Request.Search;
using HotelBooking.Domain.Response.Search;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.Search
{
    public interface ISearchService
    {
        Task<SearchResult> Search(SearchRequest request);
    }
}
