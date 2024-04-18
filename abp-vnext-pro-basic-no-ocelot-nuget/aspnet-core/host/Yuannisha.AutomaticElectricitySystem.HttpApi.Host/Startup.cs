using Hangfire.MySql;
using Yuannisha.AutomaticElectricitySystem.BuildingConsumptionsAppservice;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionIAppservice;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionsAppservice;
using Yuannisha.AutomaticElectricitySystem.HangfireWorks;
using Yuannisha.AutomaticElectricitySystem.PowerConsumptionIAppservice;
using Yuannisha.AutomaticElectricitySystem.ServiceLocatorInit;

namespace Yuannisha.AutomaticElectricitySystem
{
    public class Startup
    {
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
            // services.AddTransient <AutoOperatePowerSwitch>();
            services.AddTransient<IBuildingConsumptionAppservice, BuildingConsumptionAppservice>();
            services.AddTransient<IDailyTotalConsumptionAppservice, DailyTotalConsumptionAppservice>();
            services.AddTransient<BuildingConsumptionAppservice>();
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, 
            IServiceProvider serviceProvider, IBackgroundJobClient backgroundJobs)
        {
            // 启用 CORS
            // app.UseCors("MyAllowSpecificOrigins");
            app.InitializeApplication();
            
            ServiceLocator.SetLocatorProvider(serviceProvider);
            
            // BookingInformationAppService.InitPianoRoomsBookingTimespan();

            //系统开启时首次执行任务
            // backgroundJobs.Enqueue<AutoOperatePowerSwitch>(s => s.Test());
            backgroundJobs.Enqueue<AutoOperatePowerSwitch>(s => s.SetValuesForPowerSwitchs());

            // 调度定时任务
            RecurringJob.RemoveIfExists("Test");
            RecurringJob.RemoveIfExists("InitPowerSwitchsValue");
            RecurringJob.RemoveIfExists("AutoTeleMetering");
            RecurringJob.RemoveIfExists("AutoSetValueWithConsumption");
            RecurringJob.RemoveIfExists("AutoAddTestDatasScriptJob");
            RecurringJob.RemoveIfExists("InitTimeSpans");
            RecurringJob.RemoveIfExists("UpdateClassInformation");
            RecurringJob.RemoveIfExists("AutoOperateByTime");
            
            // var autoOperatePowerSwitch = serviceProvider.GetService<AutoOperatePowerSwitch>();
            // RecurringJob.AddOrUpdate<AutoOperatePowerSwitch>("InitPowerSwitchsValue", s => s
                // .SetValuesForPowerSwitchs(), Cron.Minutely);
            
            // RecurringJob.AddOrUpdate<AutoOperatePowerSwitch>("Test",s=>s.Test(),Cron.Minutely);
            RecurringJob.AddOrUpdate<AutoOperatePowerSwitch>("InitPowerSwitchsValue",s=>s.SetValuesForPowerSwitchs(),
                Cron.Minutely,TimeZoneInfo.Local);
            // // RecurringJob.AddOrUpdate<AutoOperatePowerSwitch>("AutoTeleMetering",s=>s.AutoTleMetering(),Cron.MinuteInterval(13));
            RecurringJob.AddOrUpdate<AutoOperatePowerSwitch>("AutoSetValueWithConsumption",s=>s
                .AutoSetValueWithConsumption(),Cron.MinuteInterval(14),TimeZoneInfo.Local);
            // RecurringJob.AddOrUpdate<AutoOperatePowerSwitch>("AutoAddTestDatasScriptJob",
            //     s=>s.AutoAddTestDatasScriptJob(),Cron.Minutely,TimeZoneInfo.Local);
            
            
            // TCP_serviceManagement.benginService();//项目启动时开启Socket服务
            //
            // DatasHandle.GetInformationOfClasses();//项目开启时获取系统数据库中的课表信息
            //
            // BookingInformationAppService.InitPianoRoomsBookingTimespan();
            //
            // RecurringJob.AddOrUpdate("UpdateClassInformation", () => DatasHandle.GetInformationOfClasses(), 
            //     Cron.Daily(0),TimeZoneInfo.Local);//每天更新课表信息(可能源课表数据库的数据有更改)
            // RecurringJob.AddOrUpdate("InitTimeSpans", () => BookingInformationAppService.InitPianoRoomsBookingTimespan(),
            //     Cron.Daily(0),TimeZoneInfo.Local);
            // RecurringJob.AddOrUpdate<AutoOperatePowerSwitch>("AutoOperateByTime", x => x.AutoOperateByTime(), 
            //     Cron.MinuteInterval(10), TimeZoneInfo.Local);
            //
            
        }
    }

}
