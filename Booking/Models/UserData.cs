using System.Data;

namespace Booking.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
      

        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }

    public class UserRoles
    {
        public int UserRolesID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
    public class Payment
    {
        public int PaymentID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int AppointmentID { get; set; }
        public Appointment Appointment { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDateTime { get; set; }

        public string PaymentStatus { get; set; }
    }
    public class MedicalRecord
    {
        public int RecordID { get; set; }

        public int PatientID { get; set; }
        public Patient Patient { get; set; }

        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime Date { get; set; }

        public string Diagnosis { get; set; }

        public string Prescription { get; set; }

        public string Notes { get; set; }
    }


    public class UserAppointments
    {
        public int UserAppointmentsID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int AppointmentID { get; set; }
        public Appointment Appointment { get; set; }
    }

}
