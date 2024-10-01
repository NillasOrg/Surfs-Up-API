using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Surfs_Up_API.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Surfs_Up_API.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        // Log the incoming email for debugging
        Console.WriteLine($"Attempting login for email: {model.Email.ToLower()}");

        // Find user by normalized email
        var user = await _userManager.FindByNameAsync(model.Email.ToLower());

        if (user == null)
        {
            Console.WriteLine($"User with email {model.Email} not found."); // Log message for debugging
            return Unauthorized("Invalid credentials");
        }
    
        // Check password validity
        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            Console.WriteLine($"Password mismatch for user {user.Email}."); // Log message for debugging
            return Unauthorized("Invalid credentials");
        }

        // Create claims and sign in
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, // Keep the user logged in
            ExpiresUtc = DateTime.UtcNow.AddMinutes(600) // Adjust duration as needed
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


        return Ok(new { Message = "Logged in successfully" });
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new User
        {
            Email = model.Email.ToLower(),
            NormalizedEmail = model.Email.ToUpper(),
            Name = model.Name,
            UserName = model.Email.ToLower(),
        }; 

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok(new { Message = "User registered successfully" });
        }

        return BadRequest(result.Errors);
    }

    // Additional login or authentication endpoints can be added here
}

public class RegisterModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}