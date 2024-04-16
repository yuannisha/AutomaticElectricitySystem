namespace Yuannisha.AutomaticElectricitySystem.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class AutomaticElectricitySystemController : AbpController
    {
        protected AutomaticElectricitySystemController()
        {
            LocalizationResource = typeof(AutomaticElectricitySystemResource);
        }
    }
}