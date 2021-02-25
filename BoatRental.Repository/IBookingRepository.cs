using BoatRental.Domain.Models;
using System.Collections.Generic;

namespace BoatRental.Repository
{
    public interface IBookingRepository
    {
        void Add(Booking booking);
        Booking Get(int bookingId);
        List<Booking> GetAll();
        bool Update(Booking booking);
    }
}
