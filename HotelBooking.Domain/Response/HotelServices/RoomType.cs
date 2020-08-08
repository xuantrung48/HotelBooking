﻿using HotelBooking.Domain.Response.Facilities;
using System.Collections.Generic;

namespace HotelBooking.Domain.Response.HotelServices
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; }
        public int DefaultPrice { get; set; }
        public float Capacity { get; set; }
        public int Quantity { get; set; }
        public bool IsDelete { get; set; }
        public string Description { get; set; }
        public IEnumerable<Facility> Facilities { get; set; }
        public IEnumerable<RoomTypeImage> Images { get; set; }
    }
}