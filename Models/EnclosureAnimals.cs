namespace ZooManagement.Models;

public class EnclosureAnimals
{
    public int EnclosureAnimalsId { get; set; }
    public string? Enclosure { get; set; }

    public List<Animal>? Animals { get; set; }
}