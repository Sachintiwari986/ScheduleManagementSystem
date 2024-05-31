using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShiftManagementSystem.Models;
using ShiftManagementSystem.Services;

namespace ShiftManagementSystem.Controllers
{
    public class WorkerToShiftController : Controller
    {
        private readonly ShiftManagementCoreDbContext _db;
        private readonly IEmailServices _emailService;
        public WorkerToShiftController(ShiftManagementCoreDbContext db, IEmailServices emailServices)
        {
            _db = db;
            _emailService = emailServices;  
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

        public IActionResult SendEmail(int workerId, int shiftId)
        {
            var worker = _db.Workers.FirstOrDefault(w => w.Id == workerId);
            var shift = _db.Shifts.FirstOrDefault(s => s.Id == shiftId);

            if (worker != null && shift != null)
            {
                var subject = "Shift Assignment Notification";
                var body = $"Hello {worker.Email},<br/><br/>You are assigned to the {shift.Name} shift.<br/><br/>Best regards,<br/>Shift Management System";

                bool emailSent = _emailService.SendEmail(worker.Email, subject, body);

                if (emailSent)
                {
                    ViewData["Message"] = "Email sent successfully.";
                }
                else
                {
                    ViewData["Message"] = "Failed to send email.";
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

