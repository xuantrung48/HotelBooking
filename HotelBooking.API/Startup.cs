using HotelBooking.API.DbContext;
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
using HotelBooking.BAL.Interface.Search;
using HotelBooking.BAL.Interface.Supports;
using HotelBooking.BAL.Promotions;
using HotelBooking.BAL.Search;
using HotelBooking.BAL.Supports;
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
using HotelBooking.DAL.Interface.Supports;
using HotelBooking.DAL.Promotions;
using HotelBooking.DAL.Supports;
using HotelBooking.Domain;
using HotelBooking.Domain.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Common.ConnectionString));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddTransient<IBookingRoomDetailsService, BookingRoomDetailsService>();
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
            services.AddTransient<IRoomTypeImageRepository, RoomTypeImageRepository>();
            services.AddTransient<IRoomTypeImageService, RoomTypeImageService>();
            services.AddTransient<ISupportRepository, SupportRepository>();
            services.AddTransient<ISupportService, SupportService>();
            services.AddTransient<IServiceImageRepository, ServiceImageRepository>();
            services.AddTransient<IServiceImageService, ServiceImageService>();
            services.AddTransient<ISearchService, SearchService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
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
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Could not find Anything.");
            });
        }
    }
}