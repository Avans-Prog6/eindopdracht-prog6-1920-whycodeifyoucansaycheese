using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;

namespace BeestjeOpJeFeestje.Controllers
{
    public class HomeController : Controller
    {
        private IBoekingRepository _bookRepo;

        public HomeController(IBoekingRepository BookRepo)
        {
            _bookRepo = BookRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ID,ContactpersonID,Date")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _bookRepo.TempBooking.Date = booking.Date;
                return RedirectToAction("Step1", "Booking");
            }
            return View(booking);
        }

    }
}