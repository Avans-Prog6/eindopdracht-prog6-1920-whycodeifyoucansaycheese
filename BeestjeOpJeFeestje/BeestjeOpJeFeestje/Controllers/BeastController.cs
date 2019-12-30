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
    public class BeastController : Controller
    {
        private IBeastRepository _beastrepo;

        public BeastController(IBeastRepository BeastRepo)
        {
            _beastrepo = BeastRepo;
        }

        // GET: Beast
        public ActionResult Index()
        {
            var beast = _beastrepo.GetAll();
            return View(beast.ToList());
        }

        // GET: Beast/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beast beast = _beastrepo.Get(id);
            if (beast == null)
            {
                return HttpNotFound();
            }
            return View(beast);
        }

        // GET: Beast/Create
        public ActionResult Create()
        {
            ViewBag.Type = new SelectList(_beastrepo.ContextDB().Type, "Type1", "Type1");
            return View();
        }

        // POST: Beast/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Type,Price")] Beast beast)
        {
            if (ModelState.IsValid)
            {
                _beastrepo.Add(beast);
                _beastrepo.Complete();
                return RedirectToAction("Index");
            }

            ViewBag.Type = new SelectList(_beastrepo.ContextDB().Type, "Type1", "Type1", beast.Type);
            return View(beast);
        }

        // GET: Beast/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beast beast = _beastrepo.Get(id);
            if (beast == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type = new SelectList(_beastrepo.ContextDB().Type, "Type1", "Type1", beast.Type);
            return View(beast);
        }

        // POST: Beast/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Type,Price")] Beast beast)
        {
            if (ModelState.IsValid)
            {
                //_beastrepo.ContextDB().Entry(beast).State = EntityState.Modified;
                //_beastrepo.ContextDB().Beast.Attach
                _beastrepo.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.Type = new SelectList(_beastrepo.ContextDB().Type, "Type1", "Type1", beast.Type);
            return View(beast);
        }

        // GET: Beast/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beast beast = _beastrepo.Get(id);
            if (beast == null)
            {
                return HttpNotFound();
            }
            return View(beast);
        }

        // POST: Beast/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beast beast = _beastrepo.Get(id);
            _beastrepo.Remove(beast);
            _beastrepo.Complete();
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
