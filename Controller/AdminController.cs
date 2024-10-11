using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surfs_Up_API.Data;
using Surfs_Up_API.Models;
using System;
using System.Net;

namespace Surfs_Up_API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdminController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //GET api/admin
        [HttpGet]
        public async Task<IActionResult> GetAllRequests()
        {
            List<Request> requests = await _context.Requests.ToListAsync();
            return Ok(requests);
        }

        //POST api/admin
        [HttpPost]
        public async Task<IActionResult> LogError(Request failedRequest)
        {
            Request? request = await _context.Requests.FirstOrDefaultAsync(x => x.IpAddress == failedRequest.IpAddress);

            if (request != null)
            {
                request.FailedRequests++;

                await _context.SaveChangesAsync();

            }

            return Ok();
        }
    }
}
