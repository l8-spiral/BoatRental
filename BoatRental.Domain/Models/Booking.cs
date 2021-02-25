using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoatRental.Domain.Models
{
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? RecievedDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public string CustmerSSN { get; set; }
        public double EstimatedPrice { get; set; }
        public double? RecievedPrice { get; set; }
        public int EstimatedHours { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("Boat")]
        public int BoatId { get; set; }
        public Boat Boat { get; set; }
    }
}
