using Surfs_Up_API.Data;
using Surfs_Up_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        //GET by ID /api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            User user = await appDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //POST api/user
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            await appDbContext.Users.AddAsync(user);
            await appDbContext.SaveChangesAsync();

            return Ok($"Created User ID: {user.Id}");
        }

        //PUT api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            User userToUpdate = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;

            await appDbContext.SaveChangesAsync();
            return Ok($"Updated User ID: {user.Id}");
        }

        //DELETE api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            await appDbContext.SaveChangesAsync();
            return Ok($"Deleted User ID: {user.Id}");
        }
    }
}
