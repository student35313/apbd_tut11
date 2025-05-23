using HW_Tutorial11.DTOs;

namespace HW_Tutorial11.Services;

public interface IDbService
{
    Task AddPrescriptionAsync(PrescriptionCreationDto creationDto);
    Task<PatientPrescriptionsDto> GetPatientByIdAsync(int patientId);
}