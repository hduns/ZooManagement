namespace ZooManagement.Models;

public class Enclosure
{
    public Enclosure()
    {
        AnimalsInEnclosure = new List<Animal>();
    }
    public int EnclosureId { get; set; }
    public string? Name { get; set; }
    public int MaxAnimals { get; set; }
    public IList<Animal>? AnimalsInEnclosure { get; set; }
}