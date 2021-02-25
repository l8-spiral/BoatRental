using BoatRental.Domain.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace BoatRental.Domain
{
    public class BoatRentalInitilizer : DropCreateDatabaseIfModelChanges<BoatRentalContext>
    {
        protected override void Seed(BoatRentalContext context)
        {
            var contxt = new BoatRentalContext();

            context.Boat.AddRange(new List<Boat>
            {
                new Boat { Category = BoatCategory.Jolle, Name = "Jolle", PricePerHour = 299, IsRented = false },
                new Boat { Category = BoatCategory.Under40, Name = "Båt < 40 fot", PricePerHour = 399, IsRented = false },
                new Boat { Category = BoatCategory.Over40, Name = "Båt >= 40 fot", PricePerHour = 699, IsRented = false }
            });

            base.Seed(context);
            context.SaveChanges();
        }
    }
}
