using BoatRental.Domain;
using BoatRental.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoatRental.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBoatRepository _boatRepository;
        public HomeController(BoatRepository boatRepository)
        {
            _boatRepository = boatRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}