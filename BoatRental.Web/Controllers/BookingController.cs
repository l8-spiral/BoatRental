using BoatRental.Domain;
using BoatRental.Domain.Models;
using BoatRental.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BoatRental.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBoatRepository _boatRepository;


        public BookingController(IBookingRepository repository, IBoatRepository boatRepository)
        {
            _bookingRepository = repository;
            _boatRepository = boatRepository;
        }

        public ActionResult Index()
        {   
            return View();
        }

        [HttpPost]
        public ActionResult BookBoat(Booking Viewbooking)

        {
            int BoatId = Convert.ToInt32(Viewbooking.BoatId);
            int TotalHours = Convert.ToInt32(Viewbooking.EstimatedHours);

            //Build booking
            var boat = _boatRepository.Get(BoatId);
            Booking booking = new Booking();

            booking.BoatId = BoatId;
            booking.CustmerSSN = Viewbooking.CustmerSSN;
            booking.StartDate = DateTime.Now;
            booking.EstimatedPrice = boat.GetPrice(TotalHours);
            booking.EstimatedEndDate = DateTime.Now.AddHours(TotalHours);
            booking.EstimatedHours = TotalHours;
            booking.IsActive = true;
            boat.IsRented = true;

            //Send back Calcelated booking with its Boat to view for confirmation

            booking.Boat = boat;

            return View ("~/Views/Booking/ConfirmBooking.cshtml", booking);
        }

        [HttpPost]
        public ActionResult ViewBooking(string bookingId)
        {
            //Get the booking from DB and return to view for recalc and confirmation.
            int BookingId = -1;
            try
            {
                BookingId = Convert.ToInt32(bookingId);
            }
            catch (Exception x){}

            var booking = _bookingRepository.Get(BookingId);

            if(booking != null)
            {
                booking.Boat = _boatRepository.Get(booking.BoatId);
                booking.RecievedDate = DateTime.Now;
                booking.IsActive = false;

                var boat = _boatRepository.Get(booking.BoatId);

                decimal BookedMinutes = ((DateTime.Now - booking.StartDate).Minutes);

                decimal BookedHouersD = Math.Ceiling(Decimal.Divide(BookedMinutes, 60));

                int BookedHouers = Convert.ToInt32(BookedHouersD);

                booking.RecievedPrice = boat.GetPrice(BookedHouers);

                return View(booking);
            }
            else
            {
                ModelState.AddModelError("Id", "Bokningsnummer stämmer ej med någon bokning!");
                return View("Index");
            }
        }
        
        [HttpPost]
        public ActionResult ConfirmBooking(Booking booking)
        {
            var boat = _boatRepository.Get(booking.BoatId);

            //Update boat (Booked)
            boat.IsRented = true;
            _boatRepository.Update(boat);

            //Add new booking
            _bookingRepository.Add(booking);

            return View("~/Views/Booking/BookingConfirmed.cshtml", booking);
        }

        public ActionResult EndBooking(Booking booking)
        {
            //End booking confirmed, update DB.
            var boat = _boatRepository.Get(booking.BoatId);

            _bookingRepository.Update(booking);

            boat.IsRented = false;
            _boatRepository.Update(boat);

            return View("Index");
        }
    }
 

    public static class Ext
    {
        public static double GetPrice(this Boat boat, int totalHours)
        {
            var grundPrice = 200;
            double returnPrice = 0;

            switch (boat.Category)
            {
                case BoatCategory.Jolle:
                    returnPrice = grundPrice + (boat.PricePerHour * totalHours);
                    break;
                case BoatCategory.Over40:
                    returnPrice = (grundPrice * 1.2) + (boat.PricePerHour * 1.3 * totalHours);
                    break;
                case BoatCategory.Under40:
                    returnPrice = (grundPrice * 1.5) + (boat.PricePerHour * 1.4 * totalHours);
                    break;
            }

            return returnPrice;
        }
    }
}