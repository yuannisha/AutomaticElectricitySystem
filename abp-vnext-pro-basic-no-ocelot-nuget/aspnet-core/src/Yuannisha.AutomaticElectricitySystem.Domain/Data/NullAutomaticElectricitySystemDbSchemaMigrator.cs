namespace Yuannisha.AutomaticElectricitySystem.Data
{
    /* This is used if database provider does't define
     * IAutomaticElectricitySystemDbSchemaMigrator implementation.
     */
    public class NullAutomaticElectricitySystemDbSchemaMigrator : IAutomaticElectricitySystemDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}