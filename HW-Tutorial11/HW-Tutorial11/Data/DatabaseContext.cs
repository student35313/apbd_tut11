using HW_Tutorial11.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_Tutorial11.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected DatabaseContext() { }

    public DatabaseContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Doctor>().HasData(new Doctor
        {
            IdDoctor = 1,
            FirstName = "Gregory",
            LastName = "House",
            Email = "house@example.com"
        });
        modelBuilder.Entity<Patient>().HasData(new Patient
        {
            IdPatient = 1,
            FirstName = "Lisa",
            LastName = "Cuddy",
            Birthdate = new DateTime(1990, 5, 10)
        });
        modelBuilder.Entity<Medicament>().HasData(new Medicament
        {
            IdMedicament = 1,
            Name = "Ibuprofen",
            Description = "Anti-inflammatory drug",
            Type = "Painkiller"
        });
        modelBuilder.Entity<Prescription>().HasData(new Prescription
        {
            IdPrescription = 1,
            Date = new DateTime(2024, 5, 1),
            DueDate = new DateTime(2024, 6, 1),
            IdPatient = 1,
            IdDoctor = 1
        });
        modelBuilder.Entity<PrescriptionMedicament>().HasData(new PrescriptionMedicament
        {
            IdMedicament = 1,
            IdPrescription = 1,
            Dose = 2,
            Details = "Take after meals"
        });
    }
}