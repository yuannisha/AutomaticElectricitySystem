namespace Yuannisha.AutomaticElectricitySystem
{
    /* Inherit your application services from this class.
     */
    public abstract class AutomaticElectricitySystemAppService : ApplicationService
    {
        protected AutomaticElectricitySystemAppService()
        {
            LocalizationResource = typeof(AutomaticElectricitySystemResource);
        }
    }
}
