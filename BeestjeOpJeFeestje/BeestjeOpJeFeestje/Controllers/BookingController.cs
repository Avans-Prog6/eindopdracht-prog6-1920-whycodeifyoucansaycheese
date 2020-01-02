﻿using System;
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

        public BookingController(IBoekingRepository boekingRepository)
        {
            _boekingRepository = boekingRepository;
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
