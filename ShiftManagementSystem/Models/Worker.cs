using System.ComponentModel.DataAnnotations;

namespace ShiftManagementSystem.Models
{
    public class Worker
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Email Address")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email Address Is Required")]
        public string Email{ get; set;}

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Is Required")]
        public string  PhoneNumber { get; set; }
    }
}
