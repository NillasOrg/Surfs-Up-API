using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Surfs_Up_API.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : IdentityUser
    {
        public int Id { get; set; }
        [StringLength(100)]
        [MaxLength(100, ErrorMessage = "Navn må maks være 50 tegn")]
        [Required(ErrorMessage = "Indtast gyldigt navn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Indtats venligst et gyldigt Email")]
        [MaxLength(50, ErrorMessage = "Email må maks være 50 tegn")]
        public string Email { get; set; }
    }
}