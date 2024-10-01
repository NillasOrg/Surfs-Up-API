using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up_API.Data;
using Surfs_Up_API.Models;

namespace Surfs_Up_API.Controller;

[ApiController]
[Route("api/[controller]")]
public class SurfboardController : ControllerBase
{
    public readonly AppDbContext _context;

    public SurfboardController(AppDbContext context)
    {
        _context = context;
    }
    
    // GET: api/surfboard
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var surfboards = await _context.Surfboards.ToListAsync();
        return Ok(surfboards);
    }

    // GET: api/surfboard/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var surfboard = await _context.Surfboards.FindAsync(id);
        if (surfboard == null)
        {
            return NotFound();
        }
        return Ok(surfboard);
    }

    // POST: api/surfboard
    [HttpPost]
    public async Task<IActionResult> Add([FromBody]Surfboard surfboard)
    {
        // Add the new person asynchronously
        await _context.Surfboards.AddAsync(surfboard);
        // Save changes asynchronously
        await _context.SaveChangesAsync();
        return Ok($"Created surfboard ID: {surfboard.Id}");
    }

    // PUT: api/surfboard/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]Surfboard surfboard)
    {
        var boardToUpdate = await _context.Surfboards.FirstOrDefaultAsync(x => x.Id == id);
        if (boardToUpdate == null)
        {
            return NotFound();
        }
        
        // Update fields
        boardToUpdate.Name = surfboard.Name;
        boardToUpdate.Description = surfboard.Description;
        boardToUpdate.Length = surfboard.Length;
        boardToUpdate.Width = surfboard.Width;
        boardToUpdate.Thickness = surfboard.Thickness;
        boardToUpdate.Volume = surfboard.Volume;
        boardToUpdate.Type = surfboard.Type;
        boardToUpdate.Price = surfboard.Price;
        boardToUpdate.Equipment = surfboard.Equipment;
        boardToUpdate.ImagePath = surfboard.ImagePath;
        boardToUpdate.Bookings = surfboard.Bookings;
        
        _context.Surfboards.Update(boardToUpdate);
        await _context.SaveChangesAsync(); // Await the save operation
        
        return Ok($"Updated surfboard ID: {id}");
    }

    // DELETE: api/surfboard/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        var surfboard = await _context.Surfboards.FirstOrDefaultAsync(x => x.Id == id);
        if (surfboard == null)
        {
            return NotFound();
        }
        
        // Remove the person and save changes
        _context.Surfboards.Remove(surfboard);
        await _context.SaveChangesAsync(); // Await the save operation
        
        return Ok($"Deleted Surfboard ID: {id}");
    }
}
