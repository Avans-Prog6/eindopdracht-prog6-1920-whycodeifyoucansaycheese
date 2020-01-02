using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Domain;

namespace BeestjeOpJeFeestje.Controllers
{
    public class HomeController : Controller
    {
        public static DateTime BookingDateTime { get; private set; }

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
                BookingDateTime = booking.Date;
                return RedirectToAction("Step1", "Booking");
            }
            return View(booking);
        }

    }
}