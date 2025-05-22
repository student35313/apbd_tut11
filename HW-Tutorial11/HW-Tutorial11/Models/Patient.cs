using System.ComponentModel.DataAnnotations;

namespace HW_Tutorial11.Models;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    public DateTime Birthdate { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; }
}