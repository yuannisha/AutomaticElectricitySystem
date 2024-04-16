namespace Yuannisha.AutomaticElectricitySystem.Settings
{
    public class AutomaticElectricitySystemSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(AutomaticElectricitySystemSettings.MySetting1));
            OverrideDefalutSettings(context);
        }

        /// <summary>
        /// 重写默认setting添加自定义属性
        /// </summary>
        private static void OverrideDefalutSettings(ISettingDefinitionContext context)
        {
            
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AutomaticElectricitySystemResource>(name);
        }
    }
}