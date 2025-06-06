namespace ZooManagement.Models;
public class AnimalType 
{
    public int AnimalTypeId { get; set; }
    public string? Species {get; set;}
    public int ClassificationId {get; set;}
    public Classification? Classification { get; set; }
}