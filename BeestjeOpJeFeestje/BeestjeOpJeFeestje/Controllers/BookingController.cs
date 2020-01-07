﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;

namespace BeestjeOpJeFeestje.Controllers
{
    public class BookingController : Controller
    {
        private readonly IAccessoryRepository _accrepo;
        private readonly IBeastRepository _beastrepo;
        private readonly IBoekingRepository _boekingRepository;
        private readonly IContactpersonRepository _contactrepo;

        public BookingController(IBoekingRepository boekingRepository, IBeastRepository BeastRepo,
            IAccessoryRepository AccRepo, IContactpersonRepository ContactRepo)
        {
            _boekingRepository = boekingRepository;
            _beastrepo = BeastRepo;
            _accrepo = AccRepo;
            _contactrepo = ContactRepo;
        }

        public BookingController(IBoekingRepository boekingRepository)
        {
            _boekingRepository = boekingRepository;
        }

        public List<Beast> AllBeasts { get; set; }

        // GET: Booking
        public ActionResult Index()
        {
            var booking = _boekingRepository.GetAll();
            return View(booking.ToList());
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var booking = _boekingRepository.Get(id);

            if (booking == null) return HttpNotFound();
            return View(booking);
        }


        // POST: Booking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var booking = _boekingRepository.Get(id);

            if (booking == null) return HttpNotFound();
            return View(booking);
        }

        #region AddAnimal

        [HttpPost]
        public ActionResult AddCheckedAnimal()
        {
            var temp = _boekingRepository.TempBooking;
            var beastie = _beastrepo.Get(int.Parse(Request.Form.Get("BeastID")));

            var beastieList = _boekingRepository.AnimalsBooked().ToList();
            if (beastieList.Contains(beastie))
            {
                beastie.Selected = "Selecteren";
                beastieList.Remove(beastie);
                temp.Beast = beastieList;
                _boekingRepository.TempBooking = temp;
                if (!_boekingRepository.PolarLionExists()) _beastrepo.ExcludeFarm = false;
                if (!_boekingRepository.FarmExists()) _beastrepo.ExcludePolarLion = false;
                InfoBar();
                return RedirectToAction("Step1");
            }

            if (beastie.Name == "Leeuw" || beastie.Name == "Ijsbeer")
                _beastrepo.ExcludeFarm = true;
            if (beastie.Type == "Boerderij")
                _beastrepo.ExcludePolarLion = true;
            beastie.Selected = "Deselecteren";
            beastieList.Add(beastie);
            temp.Beast = beastieList;
            _boekingRepository.TempBooking = temp;
            InfoBar();

            return RedirectToAction("Step1");
        }

        #endregion

        #region AddAccessory

        [HttpPost]
        public ActionResult AddCheckedAccessory()
        {
            var temp = _boekingRepository.TempBooking;
            var acc = _accrepo.Get(int.Parse(Request.Form.Get("AccID")));

            var accList = _boekingRepository.AccessoriesBooked().ToList();
            if (accList.Contains(acc))
            {
                acc.Selected = "Selecteren";
                acc.IsSelected = false;
                accList.Remove(acc);
                temp.Accessory = accList;
                _boekingRepository.TempBooking = temp;
                InfoBar();
                return RedirectToAction("Step2");
            }

            acc.Selected = "Deselecteren";
            acc.IsSelected = true;
            accList.Add(acc);
            temp.Accessory = accList;
            _boekingRepository.TempBooking = temp;
            InfoBar();

            return RedirectToAction("Step2");
        }

        #endregion

        public ActionResult InfoBar()
        {
            return View(_boekingRepository.TempBooking);
        }

        // POST: Booking/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var booking = _boekingRepository.Get(id);
            booking.Accessory.Clear();
            booking.Beast.Clear();
            var person = booking.ContactPerson;
            
            _boekingRepository.Remove(booking);
            _contactrepo.Remove(person);
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

        #region Step1

        public ActionResult Step1()
        {
            var temp = _boekingRepository.TempBooking;
            if (temp.Date.Month > 9 || temp.Date.Month < 3)
                _beastrepo.ExcludeDesert = true;
            else
                _beastrepo.ExcludeDesert = false;
            if (temp.Date.Month > 5 && temp.Date.Month < 9)
                _beastrepo.ExcludeSnow = true;
            else
                _beastrepo.ExcludeSnow = false;
            if (temp.Date.DayOfWeek == DayOfWeek.Saturday || temp.Date.DayOfWeek == DayOfWeek.Sunday)
                _beastrepo.ExcludePinguin = true;
            else
                _beastrepo.ExcludePinguin = false;
            AllBeasts = new List<Beast>(_beastrepo.BeastsAvailable(temp.Date));
            return View(AllBeasts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Step1(string z)
        {
            return RedirectToAction("Step2", "Booking");
        }

        #endregion

        #region Step2

        public ActionResult Step2()
        {
            return View(_boekingRepository.AnimalsBooked());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Step2(string z)
        {
            return RedirectToAction("Step3", "Booking");
        }

        #endregion

        #region Step3

        public ActionResult Step3()
        {
            return View();
        }

        // POST: ContactPerson/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Step3([Bind(Include = "ID,FirstName,InBetween,LastName,Adress,Email,PhoneNumber")]
            ContactPerson contactPerson)
        {
            if (ModelState.IsValid)
            {
                _contactrepo.TempPerson = contactPerson;
                _boekingRepository.TempBooking.ContactPerson = contactPerson;
                return RedirectToAction("Step4");
            }

            return View(contactPerson);
        }

        #endregion

        #region Step4

        public ActionResult Step4()
        {
            var calc = new DiscountCalculator();
            _boekingRepository.TempBooking.Discounts = calc.CalculateTotalDiscount(_boekingRepository.TempBooking);
            _boekingRepository.TempBooking.Price = calc.CalculateTotalPrice(_boekingRepository.TempBooking);
            return View(_boekingRepository.TempBooking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Step4(string z)
        {
            _contactrepo.Add(_contactrepo.TempPerson);
            _contactrepo.Complete();
            _boekingRepository.Add(_boekingRepository.TempBooking);
            _boekingRepository.Complete();
            _boekingRepository.TempBooking = new Booking();
            return RedirectToAction("Index");
        }

        #endregion
    }
}