using Yuannisha.AutomaticElectricitySystem.Jobs;

namespace Yuannisha.AutomaticElectricitySystem.Extensions
{
    public static class RecurringJobsExtensions
    {
        public static void CreateRecurringJob(this ApplicationInitializationContext context)
        {
            using var scope = context.ServiceProvider.CreateScope();
            var testJob = scope.ServiceProvider.GetService<TestJob>();
            RecurringJob.AddOrUpdate("测试Job", () => testJob.ExecuteAsync(), CronType.Minute(1), TimeZoneInfo.Local);
        }
    }
}