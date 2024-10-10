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
        public async Task<IActionResult> GetAllRequestLogs()
        {
            List<APIRequestLog> requestLogs = await _context.APIRequestLogs.ToListAsync();
            return Ok(requestLogs);
        }

        //POST api/admin
        [HttpPost]
        public async Task<IActionResult> LogError(APIRequestLog request)
        {
            APIRequestLog? apiRequestLog = await _context.APIRequestLogs.FirstOrDefaultAsync(x => x.IpAddress == request.IpAddress);

            if (apiRequestLog != null)
            {
                apiRequestLog.FailedRequests++;

                await _context.SaveChangesAsync();

            }

            return Ok();
        }
    }
}
