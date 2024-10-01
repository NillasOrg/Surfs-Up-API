using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Surfs_Up_API.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
    }
}