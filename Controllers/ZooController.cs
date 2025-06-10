using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagement.Models;

namespace ZooManagement.Controllers;


[ApiController]
[Route("[controller]")]
public class ZooController : BaseApiController
{
    private readonly ZooManagementContext _context;

    public ZooController(ZooManagementContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAnimals([FromQuery] QueryParameters parameters)
    {
        var query = _context.Animals
                    .Include(a => a.Enclosure)
                    .Include(a => a.AnimalType)
                    .ThenInclude(at => at!.Classification)
                    .AsQueryable();

        // Filtering
        if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
        {
            if (DateOnly.TryParse(parameters.SearchTerm, out DateOnly date))
            {
                query = query.Where(a => a.DateAcquired.Equals(date));
            }
            else if (int.TryParse(parameters.SearchTerm, out int searchAge))
            {
                var animals = _context.Animals;
                var foundAnimals = new List<Animal>();
                foreach (var animal in animals)
                {
                    if (CheckAnimalAge(animal, searchAge) != null)
                    {
                        foundAnimals.Add(animal);
                    }
                }
                query = query.Where(a => foundAnimals.Contains(a));
            }
            else
            {
                query = query.Where(a => a.Name!.Contains(parameters.SearchTerm) || a.AnimalType!.Species!.Contains(parameters.SearchTerm) || a.AnimalType!.Classification!.Class!.Contains(parameters.SearchTerm));
            }
        }
        ;

        // Sorting
        query = SortQuery(parameters.SortBy!, query);

        // Paging
        var skipAmount = (parameters.PageNumber - 1) * parameters.PageSize;
        query = query.Skip(skipAmount).Take(parameters.PageSize);

        return Ok(query.ToList());
    }

    public static Animal? CheckAnimalAge(Animal animal, int searchAge)
    {
        var age = DateTime.Today.Year - animal.DateOfBirth.Year;
        if (animal.DateOfBirth.ToDateTime(TimeOnly.MinValue) > DateTime.Today.AddYears(-age)) age--;
        if (searchAge == age)
        {
            return animal;
        }
        return null;
    }

    public static IQueryable<Animal> SortQuery(string sortByTerm, IQueryable<Animal> query)
    {
        if (sortByTerm == "Name") return query.OrderBy(a => a.Name);
        else if (sortByTerm == "Class") return query.OrderBy(a => a.AnimalType!.Classification!.Class);
        else if (sortByTerm == "Date of Birth") return query.OrderBy(a => a.DateOfBirth);
        else if (sortByTerm == "Date Acquired") return query.OrderBy(a => a.DateAcquired);
        else if (sortByTerm == "Enclosure") return query.OrderBy(a => a.Enclosure!.Name);
        else return query = query.OrderBy(a => a.AnimalType!.Species);
    }
}
