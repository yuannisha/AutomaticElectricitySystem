namespace Yuannisha.AutomaticElectricitySystem.RoomsIAppservice;

public class GetAllClassroomsOutPutDto
{
    /// <summary>
    /// 教室号
    /// </summary>
    public string No { get; set; }
    
    /// <summary>
    /// 教室Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 教室Id
    /// </summary>
    public Guid BuildingId { get; set; }
    
}