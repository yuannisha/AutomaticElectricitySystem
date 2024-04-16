namespace Yuannisha.AutomaticElectricitySystem.Permissions
{
    public class AutomaticElectricitySystemPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var bookingInformation = context.AddGroup(
                AutomaticElectricitySystemPermissions.BookingInformationManagementGroupName,
                L("预约信息"));
            var bookingInformationManagement = bookingInformation.AddPermission(AutomaticElectricitySystemPermissions
                .BookingInformationManagement.Default, L("预约信息管理"));
            bookingInformationManagement.AddChild(
                AutomaticElectricitySystemPermissions.BookingInformationManagement.BookingInformation.View,
                L("查看预约信息"));
            
            var bookingLimited = context.AddGroup(
                AutomaticElectricitySystemPermissions.BookingLimitedManagementGroupName,
                L("预约限制信息"));
            var bookingLimitedManagement = bookingLimited.AddPermission(AutomaticElectricitySystemPermissions
                .BookingLimitedManagement.Default, L("预约限制信息管理"));
            bookingLimitedManagement.AddChild(
                AutomaticElectricitySystemPermissions.BookingLimitedManagement.BookingLimited.View,
                L("查看预约限制信息"));
            
            var buildings = context.AddGroup(
                AutomaticElectricitySystemPermissions.BuildingsManagementGroupName,
                L("教学楼"));
            var buildingsManagement = buildings.AddPermission(AutomaticElectricitySystemPermissions
                .BuildingsManagement.Default, L("教学楼管理"));
            buildingsManagement.AddChild(
                AutomaticElectricitySystemPermissions.BuildingsManagement.Buildings.View,
                L("查看教学楼信息"));
            buildingsManagement.AddChild(AutomaticElectricitySystemPermissions.BuildingsManagement.Buildings.Create,
                L("创建教学楼"));
            buildingsManagement.AddChild(AutomaticElectricitySystemPermissions.BuildingsManagement.Buildings.Edit,
                L("编辑教学楼"));
            buildingsManagement.AddChild(AutomaticElectricitySystemPermissions.BuildingsManagement.Buildings.Delete,
                L("删除教学楼"));
            
            var powerSwitchs = context.AddGroup(
                AutomaticElectricitySystemPermissions.PowerSwitchsManagementGroupName,
                L("智能空开"));
            var powerSwitchsManagement = powerSwitchs.AddPermission(AutomaticElectricitySystemPermissions
                .PowerSwitchsManagement.Default, L("智能空开管理"));
            powerSwitchsManagement.AddChild(
                AutomaticElectricitySystemPermissions.PowerSwitchsManagement.PowerSwitchs.View,
                L("查看智能空开信息"));
            powerSwitchsManagement.AddChild(
                AutomaticElectricitySystemPermissions.PowerSwitchsManagement.PowerSwitchs.Create,
                L("创建智能空开"));
            powerSwitchsManagement.AddChild(
                AutomaticElectricitySystemPermissions.PowerSwitchsManagement.PowerSwitchs.Edit,
                L("编辑智能空开"));
            powerSwitchsManagement.AddChild(
                AutomaticElectricitySystemPermissions.PowerSwitchsManagement.PowerSwitchs.Delete,
                L("删除智能空开"));
            
            var rooms = context.AddGroup(
                AutomaticElectricitySystemPermissions.RoomsManagementGroupName,
                L("教室"));
            var roomsManagement = rooms.AddPermission(AutomaticElectricitySystemPermissions
                .RoomsManagement.Default, L("教室管理"));
            roomsManagement.AddChild(
                AutomaticElectricitySystemPermissions.RoomsManagement.Rooms.View,
                L("查看教室信息"));
            roomsManagement.AddChild(
                AutomaticElectricitySystemPermissions.RoomsManagement.Rooms.Create,
                L("创建教室"));
            roomsManagement.AddChild(
                AutomaticElectricitySystemPermissions.RoomsManagement.Rooms.Edit,
                L("编辑教室"));
            roomsManagement.AddChild(
                AutomaticElectricitySystemPermissions.RoomsManagement.Rooms.Delete,
                L("删除教室"));


        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AutomaticElectricitySystemResource>(name);
        }
    }
}