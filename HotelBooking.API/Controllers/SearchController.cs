using HotelBooking.BAL.Interface.Search;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.Domain.Request.Search;
using HotelBooking.Domain.Response.Search;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelBooking.API.Controllers
{
    [ApiController]
    public class SearchController : Controller
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<SearchResult> Search(SearchRequest request)
        {
            return await searchService.Search(request);
        }
    }
}
