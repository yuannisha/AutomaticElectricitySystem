namespace Yuannisha.AutomaticElectricitySystem.EntityFrameworkCore
{
    public class EntityFrameworkCoreAutomaticElectricitySystemDbSchemaMigrator
        : IAutomaticElectricitySystemDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreAutomaticElectricitySystemDbSchemaMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the AutomaticElectricitySystemMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<AutomaticElectricitySystemDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}