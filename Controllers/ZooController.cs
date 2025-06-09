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
                query = query.Where(a => ConvertAnimalAge(a) == searchAge);
            }
            else
            {
                query = query.Where(a => a.Name!.Contains(parameters.SearchTerm) || a.AnimalType!.Species!.Contains(parameters.SearchTerm) || a.AnimalType!.Classification!.Class!.Contains(parameters.SearchTerm));
            }
        }
        ;



        // search by species, classification (mammal/reptile/bird etc.), age (as a number not a date of birth), name and date the zoo acquired them. 

        // Sorting
        // if (!string.IsNullOrWhiteSpace(parameters.SortBy))
        // {
        //     query = query.OrderByDynamic(parameters.SortBy);
        // }

        // Paging
        var skipAmount = (parameters.PageNumber - 1) * parameters.PageSize;
        query = query.Skip(skipAmount).Take(parameters.PageSize);

        return Ok(query.ToList());
    }

    public static int ConvertAnimalAge(Animal animal)
    {
        var age = DateTime.Today.Year - animal.DateOfBirth.Year;
        if (animal.DateOfBirth.ToDateTime(TimeOnly.MinValue) > DateTime.Today.AddYears(-age)) age--;
        Console.WriteLine(age);
        return age;
    }
}



// public static int ConvertAnimalAge(Animal animal)
// {
//     var age = DateTime.Today.Year - animal.DateOfBirth.Year;
//     if (animal.DateOfBirth.ToDateTime(TimeOnly.MinValue) > DateTime.Today.AddYears(-age)) age--;
//     return age;
// }
