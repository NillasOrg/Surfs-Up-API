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
        public List<Surfboard>? Surfboards { get; set; }
        public List<Wetsuit>? Wetsuits { get; set; }
        public string Remark { get; set; }
    }

}