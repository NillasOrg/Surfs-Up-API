using System.ComponentModel.DataAnnotations;

namespace Surfs_Up_API.Models
{
    public class Booking {
        
        public int Id {get; set;}
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate {get; set;}
        public User User {get; set;}
        public List<Surfboard>? Surfboards {get; set;}
        public List<Wetsuit>? Wetsuits {get; set;}
        public string Remark {get; set;}
    }

}