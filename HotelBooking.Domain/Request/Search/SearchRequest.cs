using System;
using System.Collections.Generic;

namespace HotelBooking.Domain.Request.Search
{
    public class SearchRequest
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public IEnumerable<RoomTypeSearchRequest> RoomTypeSearchRequests { get; set; }
    }
}
