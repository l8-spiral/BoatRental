using BoatRental.Domain;
using BoatRental.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace BoatRental.Repository
{
    public class BoatRepository : IBoatRepository
    {
        private BoatRentalContext context = new BoatRentalContext();

        public void Add(Boat boat)
        {
            context.Boat.Add(boat);
            context.SaveChanges();
        }

        public Boat Get(int boatId)
        {
            return context.Boat.Find(boatId);
        }

        public List<Boat> GetAll()
        {
            return context.Boat.ToList();
        }

        public bool Update(Boat boat)
        {
            var existingBoat = Get(boat.Id);
            if (existingBoat != null)
            {
                context.Entry(existingBoat).CurrentValues.SetValues(boat);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
