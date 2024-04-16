namespace Yuannisha.AutomaticElectricitySystem.Data
{
    public interface IAutomaticElectricitySystemDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
