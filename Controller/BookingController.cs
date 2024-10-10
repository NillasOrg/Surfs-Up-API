using Surfs_Up_API.Data;
using Surfs_Up_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Surfs_Up_API.Controller;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly AppDbContext appDbContext;

    public BookingController(AppDbContext context)
    {
        appDbContext = context;
    }

    //GET api/booking
    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        var bookings = await appDbContext.Bookings
            .Include(b => b.Surfboards)
            .Include(b => b.Wetsuits)
            .Include(b => b.User)
            .ToListAsync();
        
        return Ok(bookings);
    }

    //GET by ID /api/booking/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookingById(int id)
    {
        Console.WriteLine($"ID IS HERE: {id}");
        
        var booking = await appDbContext.Bookings.
            Include(b => b.Surfboards)
            .Include(b => b.Wetsuits)
            .Include(b => b.User)
            .FirstOrDefaultAsync(b => b.Id == id);
      
        if (booking == null)
        {
            return NotFound();
        }
        return Ok(booking);
    }

    //POST api/booking
    [HttpPost]
    public async Task<IActionResult> AddBooking([FromBody] Booking booking)
    {
        // For hver surfboard, der er en del af bookingen, sørg for at den allerede findes i systemet.
        foreach (var item in booking.Surfboards)
        {
            if (appDbContext.Surfboards.Any(c => c.Id == item.Id))
            {
                appDbContext.Attach(item);
            }
        }

        // Find den eksisterende bruger baseret på email. Hvis brugeren ikke findes, oprettes den.
        var existingUser = await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == booking.User.Email);

        if (existingUser != null)
        {
            // Hvis brugeren findes, brug den eksisterende bruger.
            booking.User = existingUser;
        }
        else
        {
            // Hvis brugeren ikke findes, opret en ny.
            await appDbContext.Users.AddAsync(booking.User);
        }

        await appDbContext.Bookings.AddAsync(booking);
        await appDbContext.SaveChangesAsync();

        Console.WriteLine($"Created Booking ID: {booking.Id}");
        return Ok(booking);
    }


    //PUT api/booking/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking booking)
    {
        Booking bookingToUpdate = await appDbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);
        if (bookingToUpdate == null)
        {
            return NotFound();
        }

        bookingToUpdate.StartDate = booking.StartDate;
        bookingToUpdate.EndDate = booking.EndDate;
        bookingToUpdate.User = booking.User;
        bookingToUpdate.Surfboards = booking.Surfboards;
        bookingToUpdate.Remark = booking.Remark;
        

        await appDbContext.SaveChangesAsync();
        return Ok($"Updated Booking ID: {booking.Id}");
    }

    //DELETE api/booking/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        Booking booking = await appDbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        if (booking == null)
        {
            return NotFound();
        }

        appDbContext.Bookings.Remove(booking);
        await appDbContext.SaveChangesAsync();

        return Ok($"Deleted Booking ID: {id}");
    }
}






