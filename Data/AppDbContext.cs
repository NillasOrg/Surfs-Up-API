using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Surfs_Up_API.Models;
using static Surfs_Up_API.Models.APIRequestLog;

namespace Surfs_Up_API.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Surfboard> Surfboards { get; set; }
    public DbSet<Wetsuit> Wetsuits { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<APIRequestLog> APIRequestLogs { get; set; }
}
