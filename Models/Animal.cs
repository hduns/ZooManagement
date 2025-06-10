namespace ZooManagement.Models;

public class Animal 
{
        public Animal()
    {
        WorkAllocationForAnimal = new List<WorkAllocation>();
    }
    public int AnimalId { get; set; }
    public int AnimalTypeId { get; set; }
    public AnimalType? AnimalType { get; set; }
    public string? Name { get; set; }
    public string? Sex { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateOnly DateAcquired { get; set; }
    public DateOnly? LastDateAtZoo { get; set; } = null;
    public bool Transfer { get; set; } = false;
    public string? TransferLocation { get; set; } = null;
    public int EnclosureId { get; set; }
    public Enclosure? Enclosure { get; set; }
    // public IList<WorkAllocation>? WorkAllocationForAnimal { get; set; }
}