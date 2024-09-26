using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up_API.Data;
using Surfs_Up_API.Models;

namespace Surfs_Up_API.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    public readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }
    
    // GET: api/product
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var persons = await _context.Products.ToListAsync();
        return Ok(persons);
    }

    // GET: api/product/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var person = await _context.Products.FindAsync(id);
        if (person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }

    // POST: api/product
    [HttpPost]
    public async Task<IActionResult> Add([FromForm]Product product)
    {
        // Add the new person asynchronously
        await _context.Products.AddAsync(product);
        // Save changes asynchronously
        await _context.SaveChangesAsync();
        return Ok($"Created Person ID: {product.Id}");
    }

    // PUT: api/product/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm]Product product)
    {
        var productToUpdate = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (productToUpdate == null)
        {
            return NotFound();
        }
        
        // Update fields
        productToUpdate.Name = product.Name;
        productToUpdate.Description = product.Description;
        productToUpdate.Length = product.Length;
        productToUpdate.Width = product.Width;
        productToUpdate.Thickness = product.Thickness;
        productToUpdate.Volume = product.Volume;
        productToUpdate.Type = product.Type;
        productToUpdate.Price = product.Price;
        productToUpdate.Equipment = product.Equipment;
        productToUpdate.ImagePath = product.ImagePath;
        productToUpdate.Bookings = product.Bookings;
        
        _context.Products.Update(productToUpdate);
        await _context.SaveChangesAsync(); // Await the save operation
        
        return Ok($"Updated Product ID: {id}");
    }

    // DELETE: api/product/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        var person = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (person == null)
        {
            return NotFound();
        }
        
        // Remove the person and save changes
        _context.Products.Remove(person);
        await _context.SaveChangesAsync(); // Await the save operation
        
        return Ok($"Deleted Person ID: {id}");
    }
}
