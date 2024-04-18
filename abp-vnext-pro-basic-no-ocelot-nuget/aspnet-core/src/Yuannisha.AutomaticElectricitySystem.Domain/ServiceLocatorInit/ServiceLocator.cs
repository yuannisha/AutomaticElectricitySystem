namespace Yuannisha.AutomaticElectricitySystem.ServiceLocatorInit;

public static class ServiceLocator
{
    private static IServiceScopeFactory _scopeFactory;

    public static void SetLocatorProvider(IServiceProvider serviceProvider)
    {
        _scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
    }

    public static T Get<T>()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            return scope.ServiceProvider.GetRequiredService<T>();
        }
    }
    public static IServiceScope CreateScope()
    {
        return _scopeFactory.CreateScope();
    }
}