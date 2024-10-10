using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using Surfs_Up_API.Data;
using Surfs_Up_API.Models;
using System.Threading.Tasks;

public class APILogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public APILogMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
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

            APIRequestLog? apiRequestLog = await dbContext.APIRequestLogs.FirstOrDefaultAsync(x => x.IpAddress == ipAddress);

            if(apiRequestLog == null)
            {
                apiRequestLog = new APIRequestLog
                {
                    IpAddress = ipAddress,
                    SuccessfulRequests = 0,
                    FailedRequests = 0,
                };
                dbContext.APIRequestLogs.Add(apiRequestLog);
            }

            if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
            {
                apiRequestLog.SuccessfulRequests++;
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
    //                var apiRequestLog = new APIRequestLog
    //                {
    //                    User = user,
    //                };

    //                dbContext.APIRequestLogs.Add(apiRequestLog);
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
        return builder.UseMiddleware<APILogMiddleware>();
    }
}

