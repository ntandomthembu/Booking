namespace Booking.Models
{
    public class Patient
    {
        public int PatientID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ContactInfo { get; set; }
    }
}
