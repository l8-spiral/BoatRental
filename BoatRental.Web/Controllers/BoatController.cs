using BoatRental.Domain.Models;
using BoatRental.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoatRental.Web.Controllers
{
    public class BoatController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBoatRepository _boatRepository;


        public BoatController(IBookingRepository repository, IBoatRepository boatRepository)
        {
            _bookingRepository = repository;
            _boatRepository = boatRepository;
        }
        // GET: Boat
        public ActionResult Index()
        {
            return View(_boatRepository.GetAll().Where(x => x.IsRented == false).ToList());
        }

        public ActionResult ViewBoat(int BoatId)
        {
            var boat = _boatRepository.Get(BoatId);
            var booking = new Booking();

            booking.Boat = _boatRepository.Get(BoatId);

            return View(booking);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult Create (Boat boat)
        {
            _boatRepository.Add(boat);

            return RedirectToAction("Index");
        }
    }
}