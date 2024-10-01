using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Surfs_Up_API.Data;
using Surfs_Up_API.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SurfsUpDb")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:*") // Allow all ports on localhost
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials(); // This allows credentials to be sent
        });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/api/auth/login"; // Adjust paths as needed
    options.LogoutPath = "/api/auth/logout"; // Adjust paths as needed
    options.ExpireTimeSpan = TimeSpan.FromMinutes(600);
    options.Cookie.Path = "/"; // Set the cookie path to root
    options.Cookie.SameSite = SameSiteMode.None; // Ensure cookies are sent in cross-site requests
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Only send cookies over HTTPS
});



builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.MapControllers();
app.UseCors("AllowALl");
app.UseAuthentication();
app.UseAuthorization();

app.Run();

