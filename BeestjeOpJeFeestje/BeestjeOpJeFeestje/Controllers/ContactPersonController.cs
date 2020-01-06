using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;

namespace BeestjeOpJeFeestje.Controllers
{
    public class ContactPersonController : Controller
    {
        private readonly IContactpersonRepository _contactpersonRepository;

        public ContactPersonController(IContactpersonRepository contactpersonRepository)
        {
            _contactpersonRepository = contactpersonRepository;
        }

        // GET: ContactPerson
        public ActionResult Index()
        {
            var cp = _contactpersonRepository.GetAll();
            return View(cp.ToList());
        }

        // GET: ContactPerson/Details/5
        public ActionResult Details(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contactPerson = _contactpersonRepository.Get(id);

            if (contactPerson == null) return HttpNotFound();
            return View(contactPerson);
        }

        // GET: ContactPerson/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactPerson/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,InBetween,LastName,Adress,Email,PhoneNumber")]
            ContactPerson contactPerson)
        {
            if (ModelState.IsValid)
            {
                _contactpersonRepository.Add(contactPerson);
                _contactpersonRepository.Complete();
                return RedirectToAction("Index");
            }

            return View(contactPerson);
        }

        // GET: ContactPerson/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contactPerson = _contactpersonRepository.Get(id);

            if (contactPerson == null) return HttpNotFound();
            return View(contactPerson);
        }

        // POST: ContactPerson/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,InBetween,LastName,Adress,Email,PhoneNumber")]
            ContactPerson contactPerson)
        {
            if (ModelState.IsValid)
            {
                _contactpersonRepository.ContextDB().Entry(contactPerson).State = EntityState.Modified;
                _contactpersonRepository.Complete();
                return RedirectToAction("Index");
            }

            return View(contactPerson);
        }

        // GET: ContactPerson/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contactPerson = _contactpersonRepository.Get(id);

            if (contactPerson == null) return HttpNotFound();
            return View(contactPerson);
        }

        // POST: ContactPerson/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var contactPerson = _contactpersonRepository.Get(id);

            _contactpersonRepository.Remove(contactPerson);
            _contactpersonRepository.Complete();
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