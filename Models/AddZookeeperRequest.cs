namespace ZooManagement.Models;

public class AddZookeeperRequest 
{
    public string? ZookeeperName { get; set; }
    public List<int>? EnclosureIds { get; set; }
}