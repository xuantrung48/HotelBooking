using HotelBooking.BAL;
using HotelBooking.BAL.Bookings;
using HotelBooking.BAL.Coupons;
using HotelBooking.BAL.Facilities;
using HotelBooking.BAL.HotelServices;
using HotelBooking.BAL.Interface;
using HotelBooking.BAL.Interface.Bookings;
using HotelBooking.BAL.Interface.Coupons;
using HotelBooking.BAL.Interface.Facilities;
using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.BAL.Interface.Promotions;
using HotelBooking.BAL.Promotions;
using HotelBooking.DAL;
using HotelBooking.DAL.Bookings;
using HotelBooking.DAL.Coupons;
using HotelBooking.DAL.Facilities;
using HotelBooking.DAL.HotelServices;
using HotelBooking.DAL.Interface;
using HotelBooking.DAL.Interface.Bookings;
using HotelBooking.DAL.Interface.Coupons;
using HotelBooking.DAL.Interface.Facilities;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.DAL.Interface.Promotions;
using HotelBooking.DAL.Promotions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelBooking.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddTransient<IBookingRoomDetailsService, BookingRoomDetailsService> ();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IBookingServiceDetailsService, BookingServiceDetailsService>();
            services.AddTransient<IRoomTypeService, RoomTypeService>();
            services.AddTransient<IServiceService, ServiceService>();
            services.AddTransient<ICustomerSevice, CustomerSerice>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IBookingRoomDetailsRepository, BookingRoomDetailsRepository>();
            services.AddTransient<IBookingServiceDetailsRepository, BookingServiceDetailsRepository>();
            services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IPromotionRepository, PromotionRepository>();
            services.AddTransient<IPromotionService, PromotionService>();
            services.AddTransient<IPromotionApplyRepository, PromotionApplyRepository>();
            services.AddTransient<IPromotionApplyService, PromotionApplyService>();
            services.AddTransient<IFacilityRepository, FacilityRepository>();
            services.AddTransient<IFacilityService, FacilityService>();
            services.AddTransient<IFacilityApplyRepository, FacilityApplyRepository>();
            services.AddTransient<IFacilityApplyService, FacilityApplyService>();
            services.AddTransient<ICouponRepository, CouponRepository>();
            services.AddTransient<ICouponService, CouponService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Booking APIs");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
