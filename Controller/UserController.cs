using Microsoft.AspNetCore.Authorization;
using Surfs_Up_API.Data;
using Surfs_Up_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Surfs_Up_API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public UserController(AppDbContext dbContext)
        {
            appDbContext = dbContext;
        }

        //GET api/user
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> users = await appDbContext.Users.ToListAsync();
            return Ok(users);
        }

        //GET by ID /api/user
        [HttpGet("{email}"), Authorize]
        public async Task<IActionResult> GetUser(string email)
        {
            User? user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                Console.WriteLine("Shit");
                return NotFound();
            }
            
            var loggedInUserName = User?.Identity?.Name;
            Console.WriteLine(loggedInUserName);

            return Ok(user);
        }
    }
}
