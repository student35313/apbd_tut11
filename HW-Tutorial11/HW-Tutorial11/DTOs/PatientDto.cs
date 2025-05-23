using System.ComponentModel.DataAnnotations;

namespace HW_Tutorial11.DTOs;

public class PatientDto
{
    [Required]
    public int IdPatient { get; set; }
    [Required, MaxLength(100)]
    public string FirstName { get; set; } 
    [Required, MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    public DateTime Birthdate { get; set; }
}