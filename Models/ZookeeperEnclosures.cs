namespace ZooManagement.Models;

public class ZookeeperEnclosure
{
    public int ZookeeperId { get; set; }
    public string? Name { get; set; }
    public List<EnclosureAnimals>? EnclosureAllocations { get; set; }
}