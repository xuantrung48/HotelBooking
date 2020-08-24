using HotelBooking.Domain.Request.Booking;
using HotelBooking.Domain.Request.Promotions;
using HotelBooking.Domain.Response;
using HotelBooking.Domain.Response.Promotions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.BAL.Interface.Promotions
{
    public interface IPromotionService
    {
        Task<Promotion> GetById(int id);

        Task<IEnumerable<Promotion>> GetAll();

        Task<ActionsResult> Save(SavePromotionRequest promotion);

        Task<ActionsResult> Delete(int id);

        Task<IEnumerable<GetMaxDiscountRatesPromotionAvailable>> GetAvailable();

        Task<IEnumerable<GetMaxDiscountRatesPromotionAvailable>> GetAvailableForDate(DateTime date);
        Task<GetAvailablePromotionForDateAndRoomIdResponse> GetAvailablePromotionForDateAndRoomId(GetAvailablePromotionForDateAndRoomIdRequest request);
    }
}