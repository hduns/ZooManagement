using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagement.Models;

namespace ZooManagement.Controllers;


[ApiController]
[Route("[controller]")]
public class ZooManagementController : ControllerBase
{

    private readonly ZooManagementContext _context;

    public ZooManagementController(ZooManagementContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("Animal/{id}")]
    public async Task<ActionResult<Animal>> GetAnimalById(int id)
    {
        var animal = await _context.Animals
            .Include(a => a.Enclosure)
            .Include(a => a.AnimalType)
            .ThenInclude(at => at!.Classification)
            .FirstOrDefaultAsync(a => a.AnimalId == id);

        if (animal == null)
        {
            return NotFound(new { message = $"Animal with ID {id} not found." });
        }

        return Ok(animal);
    }

    [HttpGet]
    [Route("/Animal/All")]
    public async Task<ActionResult<List<Animal>>> GetAllAnimals()

    {
        var animals = await _context.Animals
            .Include(a => a.Enclosure)
            .Include(a => a.AnimalType)
            .ThenInclude(at => at!.Classification)
            .ToListAsync();

        if (animals == null)
        {
            return NotFound(new { message = "No animals in the zoo." });
        }

        return Ok(animals);
    }

    [HttpPost]
    [Route("Animal")]
    public async Task<IActionResult> PostAddAnimal([FromBody] AddAnimalRequest animal)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var animalTypeExists = _context.AnimalTypes.Any(a => a.Species!.Equals(animal.AnimalType));
        if (!animalTypeExists)
        {
            return BadRequest(new { message = "That animal type does not current exist on our database." });
        }

        var animalType = _context.AnimalTypes.SingleOrDefault(at => at.Species == animal.AnimalType);
        // var matchingClassification = await _context.AnimalTypes
        //         .Where(a => a.Species == animal.AnimalType)
        //         .Select(a => a.Classification).FirstOrDefaultAsync();

        var selectedEnclosure = await _context.Enclosures.FindAsync(animal.EnclosureId);
        var animalsInEnclosure = _context.Animals
            .AsNoTracking()
            .Where(a => a.Enclosure!.EnclosureId == animal.EnclosureId);
        if (animalsInEnclosure.Count() >= selectedEnclosure!.MaxAnimals)
        {
            return BadRequest(new { message = "Selected enclosure is full." });
        }

        var newAnimal = new Animal()
        {
            AnimalTypeId = animalType!.AnimalTypeId,
            Name = animal.Name,
            Sex = animal.Sex,
            DateOfBirth = animal.DateOfBirth,
            DateAcquired = animal.DateAcquired,
            EnclosureId = animal.EnclosureId
        };

        _context.Animals.Add(newAnimal);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Animal successfully added." });
    }
    

    
}