﻿namespace ShiftManagementSystem.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Location { get; set; }
        public string Task { get; set; }
        public decimal Rate { get; set; }
    }
}
