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
        // Doctor
        modelBuilder.Entity<Doctor>(d =>
        {
            d.ToTable("Doctor");
            d.HasKey(e => e.IdDoctor);
            d.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            d.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            d.Property(e => e.Email).HasMaxLength(100).IsRequired();
        });

        // Patient
        modelBuilder.Entity<Patient>(p =>
        {
            p.ToTable("Patient");
            p.HasKey(e => e.IdPatient);
            p.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            p.Property(e => e.LastName).HasMaxLength(100).IsRequired();
        });

        // Medicament
        modelBuilder.Entity<Medicament>(m =>
        {
            m.ToTable("Medicament");
            m.HasKey(e => e.IdMedicament);
            m.Property(e => e.Name).HasMaxLength(100).IsRequired();
            m.Property(e => e.Description).HasMaxLength(100).IsRequired();
            m.Property(e => e.Type).HasMaxLength(100).IsRequired();
        });

        // Prescription
        modelBuilder.Entity<Prescription>(p =>
        {
            p.ToTable("Prescription");
            p.HasKey(e => e.IdPrescription);
            p.HasOne(e => e.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(e => e.IdPatient);

            p.HasOne(e => e.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(e => e.IdDoctor);
        });

        // PrescriptionMedicament (join table with composite PK)
        modelBuilder.Entity<PrescriptionMedicament>(pm =>
        {
            pm.ToTable("Prescription_Medicament");
            pm.HasKey(e => new { e.IdMedicament, e.IdPrescription });

            pm.Property(e => e.Details).HasMaxLength(100).IsRequired();

            pm.HasOne(e => e.Prescription)
                .WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdPrescription);

            pm.HasOne(e => e.Medicament)
                .WithMany(m => m.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdMedicament);
        });

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