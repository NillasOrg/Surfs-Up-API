using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Surfs_Up_API.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : IdentityUser
    {
        [PersonalData]
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
    }
}