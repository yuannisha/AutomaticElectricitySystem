using Hangfire.MySql;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsAppservice;
using Yuannisha.AutomaticElectricitySystem.BuildingConsumptionsAppservice;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionIAppservice;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionsAppservice;
using Yuannisha.AutomaticElectricitySystem.HangfireWorks;
using Yuannisha.AutomaticElectricitySystem.PowerConsumptionIAppservice;

namespace Yuannisha.AutomaticElectricitySystem
{
    public class Startup
    {
        // private string[] corsUrl = new string[]{"http://localhost:44315/"};
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<AutomaticElectricitySystemHttpApiHostModule>();
            services.AddHangfire(config => 
                config.UseStorage(new MySqlStorage("server=localhost;database=YuannishaAutomaticElectricitySystemDB;uid=root;pwd=1q2w3E*;Allow User Variables=true;SslMode=None", new MySqlStorageOptions
                {
                    // 可选：配置 Hangfire MySQL 存储的选项
                    PrepareSchemaIfNecessary= true,
                    // TablesPrefix = "Hangfire",
                    // QueuePollInterval = TimeSpan.FromMinutes(10),
                    // TransactionTimeout = TimeSpan.FromSeconds(30)
                })));
            services.AddHangfireServer();
            // services.AddTransient<TestWork>();
            services.AddTransient <AutoOperatePowerSwitch>();
            services.AddTransient<IBuildingConsumptionAppservice, BuildingConsumptionAppservice>();
            services.AddTransient<IDailyTotalConsumptionAppservice, DailyTotalConsumptionAppservice>();
            // services.AddAbpRepository<ConsumptionAmount, Guid>();

            // services.AddCors(options =>
            // {
            //     options.AddPolicy(name: "MyAllowSpecificOrigins",
            //         builder =>
            //         {
            //             builder.WithOrigins("http://localhost:44315/")
            //                 .AllowAnyHeader()
            //                 .AllowAnyMethod();
            //         });
            // });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // 启用 CORS
            // app.UseCors("MyAllowSpecificOrigins");
            app.InitializeApplication();
            
            BookingInformationAppService.InitPianoRoomsBookingTimespan();
            
            // 调度定时任务
            RecurringJob.RemoveIfExists("Test");
            // RecurringJob.RemoveIfExists("InitPowerSwitchsValue");
            // RecurringJob.RemoveIfExists("AutoTeleMetering");
            // RecurringJob.RemoveIfExists("AutoSetValueWithConsumption");
            
            RecurringJob.AddOrUpdate<AutoOperatePowerSwitch>("Test",s=>s.Test(),Cron.Minutely);
            // RecurringJob.AddOrUpdate<AutoOperatePowerSwitch>("InitPowerSwitchsValue",s=>s.SetValuesForPowerSwitchs(),Cron.Minutely);
            // RecurringJob.AddOrUpdate<AutoOperatePowerSwitch>("AutoTeleMetering",s=>s.AutoTleMetering(),Cron.MinuteInterval(15));
            // RecurringJob.AddOrUpdate<AutoOperatePowerSwitch>("AutoSetValueWithConsumption",s=>s
            //     .AutoSetValueWithConsumption(),Cron.MinuteInterval(18));
            
            // RecurringJob.AddOrUpdate<TestWork>("test1",ser=>ser.test(),Cron.Minutely);
            
            
        }
    }

}
