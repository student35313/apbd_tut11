namespace HW_Tutorial11.DTOs;

using System.ComponentModel.DataAnnotations;

public class MedicamentDto
{
    [Required] 
    public int IdMedicament { get; set; }
    
    public string? Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Description { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Dose must be greater than zero.")]
    public int Dose { get; set; }
}
