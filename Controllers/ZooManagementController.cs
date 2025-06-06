using Microsoft.AspNetCore.Mvc;
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

    
    [HttpGet("{id}", Name = "GetAnimalById")]
    public async Task<IActionResult> Get(int id)
    {
        var animal = await _context.Animals.FindAsync(id);

        if (animal == null)
        {
            return NotFound(new { message = $"Animal with ID {id} not found." });
        }

        return Ok(animal); 
    }

}