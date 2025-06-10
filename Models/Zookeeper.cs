namespace ZooManagement.Models;

public class Zookeeper
{
    public Zookeeper()
    {
        WorkAllocations = new List<WorkAllocation>();
    }
    public int ZookeeperId { get; set; }
    public string? Name { get; set; }
    public IList<WorkAllocation>? WorkAllocations { get; set; }
}
