namespace ZooManagement.Models;

public class WorkAllocation
{
    public int WorkAllocationId { get; set; }
    public int ZookeeperId { get; set; }
    public Zookeeper? Zookeeper { get; set; }

    public int EnclosureId { get; set; }
    public Enclosure? Enclosure { get; set; }
}
