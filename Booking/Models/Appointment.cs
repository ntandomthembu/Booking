using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Xml.Linq;

namespace Booking.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }

        public int PatientID { get; set; }
        public Patient Patient { get; set; }

        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }

        public int ServiceID { get; set; }
        public Service Service { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentDateTime { get; set; }

        public string Status { get; set; } = "Pending";


      /* 
        [NotMapped]
        public object SelectedTimeSlots { get; internal set; } 

     *//*   [NotMapped]
        [Display(Name = "Selected Time Slot")]*//*
        public DateTime SelectedTimeSlot { get; set; }*/
    }
}
