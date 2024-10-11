using Surfs_Up_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Surfs_Up_API.Models
{
    public class Wetsuit : ICartItem
    {
        public int WetsuitId { get; set; }

        public double Price { get; set; } = 149;

        [Required] public SIZES Size { get; set; } = Wetsuit.SIZES.M;

        public Booking Booking { get; set; } //denne linje refererer til Booking

        [Required]
        public GENDER Gender { get; set; }
        public enum GENDER
        {
            Mand, Kvinde
        }
        public enum SIZES
        {
            XS, S, M, L, XL, XXL, XXXL
        }
    }
}
