using BoatRental.Domain.Models;
using System.Data.Entity;

namespace BoatRental.Domain
{
    public class BoatRentalContext : DbContext
    {
        public BoatRentalContext() : base("BoatRentalContext")
        {

        }

        public DbSet<Boat> Boat { get; set; }
        public DbSet<Booking> Booking { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //      modelBuilder.Entity<User>()
        //    .Property(u => u.DisplayName)
        //    .HasColumnName("display_name");
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
