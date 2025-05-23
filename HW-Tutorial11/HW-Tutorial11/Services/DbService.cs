using HW_Tutorial11.Data;
using HW_Tutorial11.DTOs;
using HW_Tutorial11.Models;
using Microsoft.EntityFrameworkCore;
using Tutorial9.Exceptions;

namespace HW_Tutorial11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task AddPrescriptionAsync(PrescriptionCreationDto creationDto)
{

    if (creationDto.DueDate < creationDto.Date)
        throw new BadHttpRequestException("DueDate cannot be earlier than Date.");
    
    var medicamentIds = creationDto.Medicaments.Select(m => m.IdMedicament).ToList();
    var existingMedicaments = await _context.Medicaments
        .Where(m => medicamentIds.Contains(m.IdMedicament))
        .Select(m => m.IdMedicament)
        .ToListAsync();

    if (existingMedicaments.Count != medicamentIds.Count)
        throw new NotFoundException("One or more medicaments do not exist.");
    
    var doctor = await _context.Doctors.FindAsync(creationDto.DoctorId);
    if (doctor == null)
        throw new NotFoundException("Doctor not found.");
    
    var patient = await _context.Patients.FindAsync(creationDto.Patient.IdPatient);
    if (patient == null)
    {
        patient = new Patient
        {
            FirstName = creationDto.Patient.FirstName,
            LastName = creationDto.Patient.LastName,
            Birthdate = creationDto.Patient.Birthdate
        };
        _context.Patients.Add(patient);
    }
    
    var prescription = new Prescription
    {
        Date = creationDto.Date,
        DueDate = creationDto.DueDate,
        Doctor = doctor,
        Patient = patient
    };

    _context.Prescriptions.Add(prescription);
    
    await _context.SaveChangesAsync();
    
    foreach (var m in creationDto.Medicaments)
    {
        var prescriptionMedicament = new PrescriptionMedicament
        {
            IdPrescription = prescription.IdPrescription,
            IdMedicament = m.IdMedicament,
            Dose = m.Dose,
            Details = m.Description
        };
        _context.PrescriptionMedicaments.Add(prescriptionMedicament);
    }

    await _context.SaveChangesAsync();
}
    
    public async Task<PatientPrescriptionsDto> GetPatientByIdAsync(int patientId)
    {
        var patient = await _context.Patients.Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPatient == patientId);

        if (patient == null)
            throw new NotFoundException("Patient not found.");

        return new PatientPrescriptionsDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions.Select(pr => new PrescriptionDto
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Doctor = new DoctorDto
                    {
                        IdDoctor = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName
                    },
                    Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentDto
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Description = pm.Details,
                        Dose = pm.Dose
                    }).ToList()
                }).ToList()
        };
    }


}