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
        List<Booking> bookings = await appDbContext.Bookings.ToListAsync();
        return Ok(bookings);
    }

    //GET by ID /api/booking/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookingById(int id)
    {
        Booking booking = await appDbContext.Bookings.FindAsync(id);
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
        await appDbContext.Bookings.AddAsync(booking);
        await appDbContext.SaveChangesAsync();

        return Ok($"Created Booking ID: {booking.Id}");
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

        await appDbContext.SaveChangesAsync();

        return Ok($"Deleted Booking ID: {id}");
    }



}






