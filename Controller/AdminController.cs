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
            List<Request> requests = await _context.Requests
                .Include(x => x.User)
                .ToListAsync();
            return Ok(requests);
        }

        //POST api/admin
        [HttpPost("failed")]
        public async Task<IActionResult> UpdateFailedRequest(Request failedRequest)
        {
            Request? request = null;
            if (failedRequest.User != null)
                request = await _context.Requests.FirstOrDefaultAsync(x => x.IpAddress == failedRequest.IpAddress && x.User.Email == failedRequest.User.Email);               
            
            else
                request = await _context.Requests.FirstOrDefaultAsync(x => x.IpAddress == failedRequest.IpAddress && x.User == null);
            

            if (request == null)
            {
                request = failedRequest;
                _context.Requests.Add(request);
            }
            
            request.FailedRequests++;
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        //POST api/admin
        [HttpPost("success")]
        public async Task<IActionResult> UpdateSuccessfulRequest(Request successRequest)
        {
            Request? request = null;
            if (successRequest.User != null)
                request = await _context.Requests.FirstOrDefaultAsync(x => x.IpAddress == successRequest.IpAddress && x.User.Email == successRequest.User.Email);               
            
            else
                request = await _context.Requests.FirstOrDefaultAsync(x => x.IpAddress == successRequest.IpAddress && x.User == null);
            

            if (request == null)
            {
                request = successRequest;
                _context.Requests.Add(request);
            }
            
            request.SuccessfulRequests++;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
