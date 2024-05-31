using Microsoft.AspNetCore.Mvc;
using ShiftManagementSystem.Models;

namespace ShiftManagementSystem.Controllers
{
    public class ShiftController : Controller
    {
        private readonly ShiftManagementCoreDbContext _db;
        public ShiftController(ShiftManagementCoreDbContext db)
        {
            _db=db; 
        }
        public ActionResult Index()
        {
            var shifts = _db.Shifts.ToList();
            return View(shifts);
        }

        public ActionResult AddShift(int? id)
        {
            if (id == 0)
            {
                return View(new Shift());
            }
            else
            {
                Shift shift = _db.Shifts.Find(id);
                return View(shift);
            }
        }

        [HttpPost]
        public ActionResult AddShift(Shift shift)
        {
            if (shift.Id == 0)
            {
                _db.Shifts.Add(shift);
            }
            else
            {
                Shift existingShift = _db.Shifts.Find(shift.Id);
                if (existingShift != null)
                {
                    existingShift.Name = shift.Name;
                    existingShift.Date = shift.Date;
                    existingShift.Time = shift.Time;
                    existingShift.Location = shift.Location;
                    existingShift.Task = shift.Task;
                    existingShift.Rate = shift.Rate;
                }
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteShift(int id)
        {
            Shift shift = _db.Shifts.Find(id);
            if (shift != null)
            {
                _db.Shifts.Remove(shift);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult SendInvitations(int id)
        {
            var shift = _db.Shifts.Find(id);
            var workers = _db.Workers.ToList();
            // Send email invitations to selected workers
            return RedirectToAction("Index");
        }
    }
}
