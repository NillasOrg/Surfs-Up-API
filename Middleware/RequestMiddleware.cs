using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using Surfs_Up_API.Data;
using Surfs_Up_API.Models;
using System.Threading.Tasks;

public class RequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public RequestMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();

        await _next(context);

        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Request? request = await dbContext.Requests.FirstOrDefaultAsync(x => x.IpAddress == ipAddress);

            if(request == null)
            {
                request = new Request
                {
                    IpAddress = ipAddress,
                    SuccessfulRequests = 0,
                    FailedRequests = 0,
                };
                dbContext.Requests.Add(request);
            }

            if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
            {
                request.SuccessfulRequests++;
            }
            
            await dbContext.SaveChangesAsync();
        }
        
    }

    //public async Task InvokeAsync(HttpContext context)
    //{

    //    if (context.User.Identity.IsAuthenticated)
    //    {
    //        using (var scope = _serviceProvider.CreateScope())
    //        {
    //            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    //            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    //            var user = await userManager.GetUserAsync(context.User);

    //            if (user != null)
    //            {
    //                var request = new Request
    //                {
    //                    User = user,
    //                };

    //                dbContext.Requests.Add(request);
    //                await dbContext.SaveChangesAsync();
    //            }
    //        }
    //    }

    //    await _next(context);
    //}

}


public static class APILogMiddlewareExtensions
{
    public static IApplicationBuilder UseAPILog(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestMiddleware>();
    }
}

