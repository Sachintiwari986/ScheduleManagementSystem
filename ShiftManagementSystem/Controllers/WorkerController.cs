using Microsoft.AspNetCore.Mvc;
using ShiftManagementSystem.Models;

namespace ShiftManagementSystem.Controllers
{
    public class WorkerController : Controller
    {
        private readonly ShiftManagementCoreDbContext _db;
        public WorkerController(ShiftManagementCoreDbContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            var shifts = _db.Workers.ToList();
            return View(shifts);
        }

        public ActionResult AddWorker(int? id)
        {
            if (id == 0)
            {
                return View(new Worker());
            }
            else
            {
                Worker worker = _db.Workers.Find(id);
                return View(worker);
            }
        }

        [HttpPost]
        public ActionResult AddWorker(Worker worker)
        {
            if (ModelState.IsValid)
            {
                if (worker.Id == 0)
                {
                    _db.Workers.Add(worker);
                }
                else
                {
                    Worker existingWorker = _db.Workers.Find(worker.Id);
                    if (existingWorker != null)
                    {
                        existingWorker.Name = worker.Name;
                        existingWorker.Email = worker.Email;
                        existingWorker.PhoneNumber = worker.PhoneNumber;
                    }
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(worker);
            }
        }

        public ActionResult DeleteWorker(int id)
        {
            Worker worker = _db.Workers.Find(id);
            if (worker != null)
            {
                _db.Workers.Remove(worker);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
