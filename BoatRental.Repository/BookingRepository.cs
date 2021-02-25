using BoatRental.Domain;
using BoatRental.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace BoatRental.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private BoatRentalContext context = new BoatRentalContext();

        public void Add(Booking booking)
        {
            context.Booking.Add(booking);
            context.SaveChanges();
        }

        public Booking Get(int bookingId)
        {
            return context.Booking.ToList().Where(x => x.Id == bookingId && x.IsActive == true).FirstOrDefault();
        }

        public List<Booking> GetAll()
        {
            return context.Booking.ToList();
        }

        public bool Update(Booking booking)
        {
            var existingBooking = Get(booking.Id);
            if (existingBooking != null)
            {
                context.Entry(existingBooking).CurrentValues.SetValues(booking);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
