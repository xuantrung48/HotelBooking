using System.Collections.Generic;

namespace HotelBooking.Domain.Response.Search
{
    public class RoomTypeSearchResultWithPricesList
    {
        public int RoomTypeId { get; set; }
        public int MinRemain { get; set; }
        public IEnumerable<RoomPriceSearchResult> RoomPriceSearchResults { get; set; }
    }
}
