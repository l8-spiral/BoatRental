using BoatRental.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoatRental.Domain.Models
{
    public class Boat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double PricePerHour { get; set; }
        public BoatCategory Category { get; set; }
        public bool IsRented { get; set; }
    }

    public enum BoatCategory
    {
        Jolle = 1,
        Over40 = 2,
        Under40 = 3
    }
}
