namespace Booking.Models
{
    public class Availability
    {
        public int AvailabilityID { get; set; }
        public int DoctorID { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

    }
}
