using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;

namespace BeestjeOpJeFeestje.Controllers
{
    public class BookingController : Controller
    {
        private IBoekingRepository _boekingRepository;
        private IBeastRepository _beastrepo;
        private List<Beast> _chosenBeasts;
        public List<Beast> AllBeasts { get; set; }
        private DateTime _bookingDateTime;

        public BookingController(IBoekingRepository boekingRepository, IBeastRepository BeastRepo)
        {
            _boekingRepository = boekingRepository;
            _beastrepo = BeastRepo;
        }

       public BookingController(IBoekingRepository boekingRepository)
        {
            _boekingRepository = boekingRepository;
            _bookingDateTime = HomeController.BookingDateTime;
        }

        // GET: Booking
        public ActionResult Index()
        {
            var booking = _boekingRepository.GetAll();
            return View(booking.ToList());
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = _boekingRepository.Get(id);

            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            ViewBag.ContactpersonID = new SelectList(_boekingRepository.ContextDB().ContactPerson, "ID", "FirstName");
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ContactpersonID,Date")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _boekingRepository.Add(booking);
                _boekingRepository.Complete();
                return RedirectToAction("Index");
            }

            ViewBag.ContactpersonID = new SelectList(_boekingRepository.ContextDB().ContactPerson, "ID", "FirstName", booking.ContactpersonID);
            return View(booking);
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = _boekingRepository.Get(id);

            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactpersonID = new SelectList(_boekingRepository.ContextDB().ContactPerson, "ID", "FirstName", booking.ContactpersonID);
            return View(booking);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ContactpersonID,Date")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _boekingRepository.ContextDB().Entry(booking).State = EntityState.Modified;
                _boekingRepository.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.ContactpersonID = new SelectList(_boekingRepository.ContextDB().ContactPerson, "ID", "FirstName", booking.ContactpersonID);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = _boekingRepository.Get(id);

            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        public ActionResult Step1()
        {
            AllBeasts = new List<Beast>(_beastrepo.GetAll());
            return View(AllBeasts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Step1(List<Beast> beast)
        {
            _chosenBeasts = new List<Beast>();
            foreach (var item in beast)
            {
                if (item.IsChecked == true)
                {
                    _chosenBeasts.Add(item);
                }
            }

            return RedirectToAction("Step2", "Booking");
        }

        public ActionResult Step2()
        {
           
            return View(_chosenBeasts);
        }
        //[HttpPost, ActionName("Step2")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Step2([Bind(Include = "ID,Name,Type,Price,IsChecked")] IEnumerable<Beast> beast)
        //{
        //    var templist = new List<Beast>();
        //    foreach(var item in beast)
        //    {
        //        if(item.IsChecked == true)
        //        {
        //            templist.Add(item);
        //        }
        //    }

        //    return View(templist);
        //}

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = _boekingRepository.Get(id);

            _boekingRepository.Remove(booking);

            _boekingRepository.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
