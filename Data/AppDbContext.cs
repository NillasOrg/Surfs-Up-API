using Microsoft.EntityFrameworkCore;
using Surfs_Up_API.Models;

namespace Surfs_Up_API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<User> Users { get; set; }
}