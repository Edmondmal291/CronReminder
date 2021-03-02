using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CronReminder.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using CronReminder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;

namespace CronReminder
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
            services.AddRazorPages();
            services.AddTransient<IReminderRepository, ReminderRepository>();
            services.AddTransient<ISendRemindrRepository, SendReminderRepository>();
            var databaseString = Configuration["ConnectionStrings:CronReminderDB_Connection"];
            services.AddDbContext<CronReminderContext>(options =>
            options.UseSqlServer(databaseString));
            services.AddHangfire(x => x.UseSqlServerStorage(databaseString));
            services.AddHangfireServer();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            app.UseHangfireDashboard("/mydashboard");

            RecurringJob.AddOrUpdate(() =>serviceProvider.GetService<ISendRemindrRepository>().SendReminder(), Cron.Daily(5,0),TimeZoneInfo.Local);
        }
    }
}
