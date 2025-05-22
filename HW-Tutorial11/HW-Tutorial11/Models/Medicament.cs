using System.ComponentModel.DataAnnotations;

namespace HW_Tutorial11.Models;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string Description { get; set; } = null!;

    [MaxLength(100)]
    public string Type { get; set; } = null!;

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}