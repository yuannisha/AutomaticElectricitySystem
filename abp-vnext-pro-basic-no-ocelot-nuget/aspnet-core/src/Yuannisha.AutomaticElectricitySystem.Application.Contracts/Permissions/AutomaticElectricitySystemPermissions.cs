namespace Yuannisha.AutomaticElectricitySystem.Permissions
{
    public static class AutomaticElectricitySystemPermissions
    {
        public const string BookingInformationManagementGroupName = "BookingInformation";
        public const string BookingLimitedManagementGroupName = "BookingLimited";
        public const string BuildingsManagementGroupName = "Buildings";
        public const string PowerSwitchsManagementGroupName = "PowerSwitchs";
        public const string RoomsManagementGroupName = "Rooms";
        
        public static class BookingLimitedManagement
        {
            public const string Default = BookingLimitedManagementGroupName + ".BookingLimitedManagement";
            
            public static class BookingLimited
            {
                /// <summary>
                /// 查看
                /// </summary>
                public const string View = Default + ".view";
            }
        }
        
        public static class BookingInformationManagement
        {
            public const string Default = BookingInformationManagementGroupName + ".BookingInformationManagement";
            
            public static class BookingInformation
            {
                /// <summary>
                /// 查看
                /// </summary>
                public const string View = Default + ".view";
            }
        }
        
        public static class BuildingsManagement
        {
            public const string Default = BuildingsManagementGroupName + ".BuildingsManagement";
            
            public static class Buildings
            {
                /// <summary>
                /// 查看
                /// </summary>
                public const string View = Default + ".view";
                
                /// <summary>
                /// 创建
                /// </summary>
                public const string Create = Default + ".create";
                
                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = Default + ".edit";
                
                /// <summary>
                /// 删除
                /// </summary>
                public const string Delete = Default + ".delete";
            }
        }
        
        public static class PowerSwitchsManagement
        {
            public const string Default = PowerSwitchsManagementGroupName + ".PowerSwitchsManagement";
            
            public static class PowerSwitchs
            {
                /// <summary>
                /// 查看
                /// </summary>
                public const string View = Default + ".view";
                
                /// <summary>
                /// 创建
                /// </summary>
                public const string Create = Default + ".create";
                
                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = Default + ".edit";
                
                /// <summary>
                /// 删除
                /// </summary>
                public const string Delete = Default + ".delete";
            }
        }
        
        public static class RoomsManagement
        {
            public const string Default = RoomsManagementGroupName + ".RoomsManagement";
            
            public static class Rooms
            {
                /// <summary>
                /// 查看
                /// </summary>
                public const string View = Default + ".view";
                
                /// <summary>
                /// 创建
                /// </summary>
                public const string Create = Default + ".create";
                
                /// <summary>
                /// 编辑
                /// </summary>
                public const string Edit = Default + ".edit";
                
                /// <summary>
                /// 删除
                /// </summary>
                public const string Delete = Default + ".delete";
            }
        }
    }
}