using HotelBooking.BAL.Interface.Search;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.DAL.Interface.Promotions;
using HotelBooking.Domain.Request.Booking;
using HotelBooking.Domain.Request.Search;
using HotelBooking.Domain.Response.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Search
{
    public class SearchService : ISearchService
    {
        private readonly IRoomTypeRepository roomTypeRepository;
        private readonly IPromotionRepository promotionRepository;
        public SearchService(IRoomTypeRepository roomTypeRepository, IPromotionRepository promotionRepository)
        {
            this.roomTypeRepository = roomTypeRepository;
            this.promotionRepository = promotionRepository;
        }
        public IEnumerable<DateTime> EachDate(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        public async Task<SearchResult> Search(SearchRequest request)
        {
            var roomTypes = (await roomTypeRepository.GetAll()).ToList();
            var roomSearchResults = new List<RoomSearchResult>();
            foreach (var roomRequest in request.RoomTypeSearchRequests)
            {
                var roomSearchResult = new RoomSearchResult()
                {
                    Adults = roomRequest.Adults,
                    Children = roomRequest.Children
                };
                var searchModel = new SearchModel()
                {
                    CheckinDate = request.CheckInDate,
                    CheckoutDate = request.CheckOutDate,
                    NumberofAdults = roomRequest.Adults,
                    NumberofChildren = roomRequest.Children
                };
                var rooms = new List<RoomTypeSearchResultWithPricesList>();
                var roomTypeSearchResults = (await roomTypeRepository.Search(searchModel)).ToList();
                foreach (var roomTypeSearchResult in roomTypeSearchResults)
                {
                    var prices = new List<RoomPriceSearchResult>();
                    foreach (var date in EachDate(request.CheckInDate, request.CheckOutDate.AddDays(-1)))
                    {
                        foreach (var roomType in roomTypes)
                        {
                            if (roomType.RoomTypeId == roomTypeSearchResult.RoomTypeId)
                            {
                                float discountRates = await promotionRepository.GetAvailablePromotionForDateAndRoomId(new GetAvailablePromotionForDateAndRoomIdRequest()
                                {
                                    Date = date,
                                    RoomTypeId = roomTypeSearchResult.RoomTypeId
                                });
                                prices.Add(new RoomPriceSearchResult()
                                {
                                    Date = date,
                                    Price = (int)(roomType.DefaultPrice * (1 - discountRates))
                                });
                            }
                        }
                    }
                    rooms.Add(new RoomTypeSearchResultWithPricesList()
                    {
                        RoomTypeId = roomTypeSearchResult.RoomTypeId,
                        MinRemain = roomTypeSearchResult.MinRemain,
                        RoomPriceSearchResults = prices
                    });
                }
                roomSearchResult.RoomTypeSearchResults = rooms.OrderBy(roomType => roomType.RoomPriceSearchResults.Sum(price => price.Price));
                roomSearchResults.Add(roomSearchResult);
            }

            var result = new SearchResult()
            {
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                RoomSearchResults = roomSearchResults
            };
            return result;
        }
    }
}
