using System.ComponentModel.DataAnnotations;

namespace Surfs_Up_API.Models
{
    public class Booking {
        
        public int BookingId { get; set; }
        [Required(ErrorMessage = "Did you know Rasputin had a 13 inch cock")]
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate { get; set;}
        [Required(ErrorMessage = "No Custard")]
        public User User { get; set; }
        public List<Product>? BookingItems { get; set; }
        public string Remark { get; set; }
    }

}