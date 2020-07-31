using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.BAL;
using HotelBooking.BAL.Bookings;
using HotelBooking.BAL.HotelServices;
using HotelBooking.BAL.Interface;
using HotelBooking.BAL.Interface.Bookings;
using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.DAL;
using HotelBooking.DAL.Bookings;
using HotelBooking.DAL.HotelServices;
using HotelBooking.DAL.Interface;
using HotelBooking.DAL.Interface.Bookings;
using HotelBooking.DAL.Interface.HotelServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            /*services.AddTransient<IBookingService, BookingService>();*/
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
