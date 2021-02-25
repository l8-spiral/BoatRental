using BoatRental.Domain.Models;
using System.Collections.Generic;

namespace BoatRental.Repository
{
    public interface IBoatRepository
    {
        void Add(Boat boat);
        Boat Get(int boatId);
        List<Boat> GetAll();
        bool Update(Boat boat);
    }
}
