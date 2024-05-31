namespace ShiftManagementSystem.Models
{
    public class WorkerToShift
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int ShiftId { get; set; }
        public Worker Worker { get; set; }
        public Shift Shift { get; set; }

    }
}
