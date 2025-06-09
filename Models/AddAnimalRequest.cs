namespace ZooManagement.Models;

public class AddAnimalRequest 
{
    // public string? Class { get; set; }
    public string? AnimalType { get; set; }
    public string? Name { get; set; }
    public string? Sex { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateOnly DateAcquired { get; set; }
    public int EnclosureId { get; set; }
}