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
    public class AccessoryController : Controller
    {

        private IAccessoryRepository _accessRepo;

        public AccessoryController(IAccessoryRepository AccessRepo)
        {
            _accessRepo = AccessRepo;
        }
        // GET: Accessory
        public ActionResult Index()
        {
            var accessory = _accessRepo.GetAll();
            return View(accessory.ToList());
        }

        // GET: Accessory/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accessory accessory = _accessRepo.Get(id);
            if (accessory == null)
            {
                return HttpNotFound();
            }
            return View(accessory);
        }

        // GET: Accessory/Create
        public ActionResult Create()
        {
            ViewBag.BeastID = new SelectList(_accessRepo.ContextDB().Beast, "ID", "Name");
            return View();
        }

        // POST: Accessory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,BeastID")] Accessory accessory)
        {
            if (ModelState.IsValid)
            {
                _accessRepo.Add(accessory);
                _accessRepo.Complete();
                return RedirectToAction("Index");
            }

            ViewBag.BeastID = new SelectList(_accessRepo.ContextDB().Beast, "ID", "Name", accessory.BeastID);
            return View(accessory);
        }

        // GET: Accessory/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accessory accessory = _accessRepo.Get(id);
            if (accessory == null)
            {
                return HttpNotFound();
            }
            ViewBag.BeastID = new SelectList(_accessRepo.ContextDB().Beast, "ID", "Name", accessory.BeastID);
            return View(accessory);
        }

        // POST: Accessory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,BeastID")] Accessory accessory)
        {
            if (ModelState.IsValid)
            {
                _accessRepo.ContextDB().Entry(accessory).State = EntityState.Modified;
                _accessRepo.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.BeastID = new SelectList(_accessRepo.ContextDB().Beast, "ID", "Name", accessory.BeastID);
            return View(accessory);
        }

        // GET: Accessory/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accessory accessory = _accessRepo.Get(id);
            if (accessory == null)
            {
                return HttpNotFound();
            }
            return View(accessory);
        }

        // POST: Accessory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accessory accessory = _accessRepo.Get(id);
            _accessRepo.Remove(accessory);
            _accessRepo.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // lol niks
            }
            base.Dispose(disposing);
        }
    }
}
