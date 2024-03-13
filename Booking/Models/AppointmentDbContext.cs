
using Booking.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


public class AppointmentDbContext : IdentityDbContext
{
 
    public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options)
        : base(options)
    {
    }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRoles> UserRoles { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
    public DbSet<UserAppointments> UserAppointments { get; set; }
    public DbSet<Availability> Availabilities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships, keys, and other entity configurations

        // Appointment

       
        modelBuilder.Entity<Appointment>()
            .HasKey(a => a.AppointmentID);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany()
            .HasForeignKey(a => a.PatientID);
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany()
            .HasForeignKey(a => a.DoctorID);

        modelBuilder.Entity<Appointment>()
    .Property(a => a.ServiceID)
    .HasColumnName("ServiceID");

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Service)
            .WithMany()
            .HasForeignKey(a => a.ServiceID);

        // Patient
        modelBuilder.Entity<Patient>()
            .HasKey(p => p.PatientID);

        // Doctor
        modelBuilder.Entity<Doctor>()
            .HasKey(d => d.DoctorID);

        // Service
        modelBuilder.Entity<Service>()
            .HasKey(s => s.ServiceID);

        
        // Role
        modelBuilder.Entity<Role>()
            .HasKey(r => r.RoleID);

        // USer
        modelBuilder.Entity<User>()
     .HasOne(u => u.Role)
     .WithMany()
     .HasForeignKey(u => u.RoleID);


        // Payment
        modelBuilder.Entity<Payment>()
            .HasKey(p => p.PaymentID);
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserID);
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Appointment)
            .WithMany()
            .HasForeignKey(p => p.AppointmentID);

        // MedicalRecord
        modelBuilder.Entity<MedicalRecord>()
            .HasKey(mr => mr.RecordID);
        modelBuilder.Entity<MedicalRecord>()
            .HasOne(mr => mr.Patient)
            .WithMany()
            .HasForeignKey(mr => mr.PatientID);
        modelBuilder.Entity<MedicalRecord>()
            .HasOne(mr => mr.Doctor)
            .WithMany()
            .HasForeignKey(mr => mr.DoctorID);

        // UserAppointments
        modelBuilder.Entity<UserAppointments>()
            .HasKey(ua => ua.UserAppointmentsID);
        modelBuilder.Entity<UserAppointments>()
            .HasOne(ua => ua.User)
            .WithMany()
            .HasForeignKey(ua => ua.UserID);
        modelBuilder.Entity<UserAppointments>()
            .HasOne(ua => ua.Appointment)
            .WithMany()
            .HasForeignKey(ua => ua.AppointmentID);

      

        base.OnModelCreating(modelBuilder);
    }
}
