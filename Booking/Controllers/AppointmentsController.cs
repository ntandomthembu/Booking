using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Booking.Models;
using System.Text.Json;

namespace Booking.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppointmentDbContext _context;

        public AppointmentsController(AppointmentDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appointmentDbContext = _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).Include(a => a.Service);
            return View();
        }

        public IActionResult AllAppointments()
        {
            var appointments = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Service)
                .ToList();

            return View(appointments);
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Creates
        /*  public IActionResult Create()
          {
              ViewData["DoctorID"] = new SelectList(_context.Doctors, "DoctorID", "FirstName");
              ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FirstName");
              ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName");
              return View();
          }*/
        /*  public IActionResult Create()
          {
              ViewData["DoctorID"] = new SelectList(_context.Doctors, "DoctorID", "FirstName");
              ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FirstName");
              ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName");


              DateTime currentDate = DateTime.Today;

              int doctorId = 1; 
              var availableTimeSlots = GetAvailableTimeSlots(currentDate, doctorId);
              ViewData["AvailableTimeSlots"] = availableTimeSlots;


              ViewData["Patients"] = _context.Patients.ToList();
              ViewData["Services"] = _context.Services.ToList();

              return View();
          }*/
        /*     public IActionResult CreateStep1()
             {
                 // Create a new Appointment model to hold user input
                 var newAppointment = new Appointment() { Status = "Pending" };

                 // Populate data for dropdowns
                 ViewData["DoctorID"] = new SelectList(_context.Doctors, "DoctorID", "FirstName");
                 ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName");

                 // Retrieve existing appointments for the selected doctor
                 var existingAppointments = _context.Appointments
                     .Where(a => a.DoctorID == newAppointment.DoctorID)
                     .Select(a => a.AppointmentDateTime)
                     .ToList();

                 // Get available time slots
                 var availableTimeSlots = GetAvailableTimeSlots(existingAppointments);

                 ViewData["AvailableTimeSlots"] = availableTimeSlots;

                 return View(newAppointment);
             }

             private List<DateTime> GetAvailableTimeSlots(List<DateTime> existingAppointments)
             {
                 // Adjust this logic based on your requirements
                 var startDate = DateTime.Now;  // Use current date and time or a specific start date and time
                 var endDate = startDate.AddDays(30);  // Use a specific end date

                 var existingDates = existingAppointments.Select(appointment => appointment.Date).Distinct().ToList();

                 var availableTimeSlots = new List<DateTime>();

                 for (var date = startDate; date <= endDate; date = date.AddHours(1))
                 {
                     // Only add if the candidate time slot is not in the list of existing dates
                     if (!existingDates.Contains(date) && date.Minute == 0)
                     {
                         availableTimeSlots.Add(date);
                     }
                 }

                 return availableTimeSlots;
             }



             [HttpPost]
             [ValidateAntiForgeryToken]
             public IActionResult CreateStep1(Appointment newAppointment)
             {

                 TempData["Step1Data"] = JsonSerializer.Serialize(newAppointment);
                 var finalAppointment = new Appointment
                 {
                     PatientID = newAppointment.PatientID,
                     DoctorID = newAppointment.DoctorID,
                     ServiceID = newAppointment.ServiceID,
                     Status = newAppointment.Status,

                     AppointmentDateTime = newAppointment.AppointmentDateTime,

                 };
                 _context.Appointments.Add(finalAppointment);
                 _context.SaveChanges();
                 return RedirectToAction("AllAppointments");
             }
     */
        public IActionResult CreateStep1()
        {
            // Create a new Appointment model to hold user input
            var newAppointment = new Appointment() { Status = "Pending" };

            // Populate data for dropdowns
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "DoctorID", "FirstName");
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName");

            // Retrieve existing appointments for the selected doctor
            var existingAppointments = _context.Appointments
                .Where(a => a.DoctorID == newAppointment.DoctorID)
                .Select(a => a.AppointmentDateTime)
                .ToList();

            // Get available time slots
            var availableTimeSlots = GetAvailableTimeSlots(existingAppointments);

            ViewData["AvailableTimeSlots"] = availableTimeSlots;

            return View(newAppointment);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStep1(Appointment newAppointment)
        {
            // Get the selected time slot from the form
            var selectedTimeSlot = newAppointment.AppointmentDateTime;

            // Remove the selected time slot from the list of available time slots
            var availableTimeSlots = ((List<DateTime>)ViewData["AvailableTimeSlots"])
                .Where(timeSlot => !timeSlot.Equals(selectedTimeSlot))
                .ToList();

            ViewData["AvailableTimeSlots"] = availableTimeSlots;


            // Other logic for saving the appointment or redirecting
            // ...

            return RedirectToAction("CreateStep2");
        }

        private List<DateTime> GetAvailableTimeSlots(List<DateTime> existingAppointments)
        {
            // Adjust this logic based on your requirements
            var startDate = DateTime.Now;  // Use current date and time or a specific start date and time
            var endDate = startDate.AddDays(30);  // Use a specific end date

            var existingDates = existingAppointments.Select(appointment => appointment.Date).Distinct().ToList();

            var availableTimeSlots = new List<DateTime>();

            for (var date = startDate; date <= endDate; date = date.AddHours(1))
            {
                // Only add if the candidate time slot is not in the list of existing dates
                if (!existingDates.Contains(date) && date.Minute == 0)
                {
                    availableTimeSlots.Add(date);
                }
            }
            Console.WriteLine("Available Time Slots:");
            foreach (var timeSlot in availableTimeSlots)
            {
                Console.WriteLine(timeSlot.ToString("yyyy-MM-dd HH:mm"));
            }

            return availableTimeSlots;
        }

        /*    public IActionResult CreateStep2()
            {
                // Retrieve and deserialize user input from TempData
                var step1DataJson = TempData["Step1Data"] as string;

                if (step1DataJson == null)
                {
                    // If TempData is missing, redirect to Step 1
                    return RedirectToAction("CreateStep1");
                }

                var step1Data = JsonSerializer.Deserialize<Appointment>(step1DataJson);

                // Get available time slots for the selected date and doctor
                var availableTimeSlots = GetAvailableTimeSlots(step1Data.AppointmentDateTime, step1Data.DoctorID);

                // Pass the available time slots to the view
                ViewData["AvailableTimeSlots"] = availableTimeSlots;

                return View(step1Data);
            }
    *//*
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult CreateStep2(Appointment step1Data)
            {
                // Validate and process the input for Step 2

                    // Combine Step 1 and Step 2 data and save the appointment
                    var finalAppointment = new Appointment
                    {
                        PatientID = step1Data.PatientID,
                        DoctorID = step1Data.DoctorID,
                        ServiceID = step1Data.ServiceID,
                        Status = step1Data.Status,

                        AppointmentDateTime = (DateTime)step1Data.AppointmentDateTime,

                    };

                    // Save the final appointment to the database or perform other actions
                    _context.Appointments.Add(finalAppointment);
                      _context.SaveChanges();

                    // Redirect to the appointments index or a confirmation page
                    return RedirectToAction("AllAppointments");


                // If validation fails, pass available time slots to the view
                ViewData["AvailableTimeSlots"] = GetAvailableTimeSlots(step1Data.AppointmentDateTime, step1Data.DoctorID);

                return View(step1Data);
            }
    */



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentID,PatientID,DoctorID,ServiceID,AppointmentDateTime,Status")] Appointment appointment)
        {
            try
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                // Handle any specific database-related exceptions here, if needed.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            ViewData["DoctorID"] = new SelectList(_context.Doctors, "DoctorID", "FirstName", appointment.DoctorID);
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FirstName", appointment.PatientID);
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName", appointment.ServiceID);

            return View(appointment);
        }




        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            var Details = _context.Patients.Where(p => p.PatientID == appointment.PatientID).FirstOrDefault();
            var Details2 = _context.Doctors.Where(p => p.DoctorID == appointment.PatientID).FirstOrDefault();
            var Details3 = _context.Services.Where(p => p.ServiceID == appointment.PatientID).FirstOrDefault();
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "DoctorID", "FirstName", Details.FirstName);
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FirstName", Details2.FirstName);
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName", Details3.ServiceName);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentID,PatientID,DoctorID,ServiceID,AppointmentDateTime,Status")] Appointment appointment)
        {
            if (id != appointment.AppointmentID)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            var Details = _context.Patients.Where(p => p.PatientID == appointment.PatientID).FirstOrDefault();
            var Details2 = _context.Doctors.Where(p => p.DoctorID == appointment.PatientID).FirstOrDefault();
            var Details3 = _context.Services.Where(p => p.ServiceID == appointment.PatientID).FirstOrDefault();
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "DoctorID", "FirstName", Details.FirstName);
            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FirstName", Details2.FirstName);
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName", Details3.ServiceName);
            return View(appointment);
        }
        private bool AppointmentExists(int id)
        {
            return (_context.Appointments?.Any(e => e.AppointmentID == id)).GetValueOrDefault();
        }
        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'AppointmentDbContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

   /*     private List<AvailableTimeSlots> GetAvailableTimeSlots(DateTime date, int doctorId)
        {
            var doctorAvailability = GetDoctorAvailability(doctorId, date.DayOfWeek);
            var bookedAppointments = _context.Appointments
                .Where(a => a.DoctorID == doctorId && a.AppointmentDateTime.Date == date && a.Status == "Booked")
                .Select(a => new AvailableTimeSlots { StartTime = a.AppointmentDateTime, EndTime = a.AppointmentDateTime.AddHours(1) })
                .ToList();

            var availableTimeSlots = new List<AvailableTimeSlots>();

            DateTime currentSlotStart = doctorAvailability;

            while (currentSlotStart.Date == date.Date)
            {
                if (!bookedAppointments.Any(a => a.StartTime <= currentSlotStart && a.EndTime > currentSlotStart))
                {
                    availableTimeSlots.Add(new AvailableTimeSlots
                    {
                        StartTime = currentSlotStart,
                        EndTime = currentSlotStart.AddHours(1)
                    });
                }

                currentSlotStart = currentSlotStart.AddHours(1);
            }

            return availableTimeSlots;
        }
*/


        private DateTime GetDoctorAvailability(int doctorId, DayOfWeek dayOfWeek)
        {
            
            var doctorAvailability = _context.Availabilities
                .FirstOrDefault(a => a.DoctorID == doctorId && a.DayOfWeek == dayOfWeek.ToString());

            if (doctorAvailability != null)
            {
               
                TimeSpan availabilityStartTime = doctorAvailability.StartTime;

               
                DateTime availabilityDateTime = DateTime.Today.Add(availabilityStartTime);

                return availabilityDateTime;
            }

           
            return DateTime.Today;
        }


    }
}
