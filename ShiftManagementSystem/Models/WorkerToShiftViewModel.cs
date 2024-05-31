using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ShiftManagementSystem.Models
{
    public class WorkerToShiftViewModel
    {
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public int ShiftId { get; set; }
        public IEnumerable<SelectListItem> Workers { get; set; }
        public IEnumerable<SelectListItem> Shifts { get; set; }
    }
}
