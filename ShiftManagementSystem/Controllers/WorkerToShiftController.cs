using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShiftManagementSystem.Models;

namespace ShiftManagementSystem.Controllers
{
    public class WorkerToShiftController : Controller
    {
        private readonly ShiftManagementCoreDbContext _db;
        public WorkerToShiftController(ShiftManagementCoreDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var workerShiftAssignments = _db.WorkerToShifts
                .Include(ws => ws.Worker)
                .Include(ws => ws.Shift)
                .ToList();

            return View(workerShiftAssignments);
        }
        public IActionResult AssignUser()
        {
            var viewModel = new WorkerToShiftViewModel
            {
                Workers = _db.Workers.Select(w => new SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = w.Email
                }).ToList(),

                Shifts = _db.Shifts.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name 
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignUser(WorkerToShiftViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var workerToShift = new WorkerToShift
                {
                    WorkerId = viewModel.WorkerId,
                    ShiftId = viewModel.ShiftId
                };

                _db.WorkerToShifts.Add(workerToShift);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            viewModel.Workers = _db.Workers.Select(w => new SelectListItem
            {
                Value = w.Id.ToString(),
                Text = w.Email
            }).ToList();

            viewModel.Shifts = _db.Shifts.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            return View(viewModel);
        }
    }
}

