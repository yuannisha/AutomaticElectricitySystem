namespace Yuannisha.AutomaticElectricitySystem.BuildingsShared;

public class GetBuildingOutPutDto
{
    /// <summary>
    /// 楼栋名称
    /// </summary>
    public string Name { get; set; }
    
    
    /// <summary>
    /// 楼栋Id
    /// </summary>
    public Guid Id { get; set; }
}