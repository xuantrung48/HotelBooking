using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Domain.Request.HotelServices
{
    public class CreateRoomTypeFacilitiesApplyRequest
    {
        public int RoomTypeId { get; set; }
        public string[] FacilitieIds { get; set; }
    }
}
