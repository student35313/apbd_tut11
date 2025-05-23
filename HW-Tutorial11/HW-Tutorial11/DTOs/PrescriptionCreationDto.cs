namespace HW_Tutorial11.DTOs;

using System.ComponentModel.DataAnnotations;

public class PrescriptionCreationDto
{
    [Required]
    public PatientDto Patient { get; set; }

    [Required]
    [MaxLength(10, ErrorMessage = "Cannot assign more than 10 medicaments.")]
    public List<MedicamentDto> Medicaments { get; set; } = new();

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public int DoctorId { get; set; }
}
