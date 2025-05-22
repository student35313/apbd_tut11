using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW_Tutorial11.Models;

public class PrescriptionMedicament
{
    [Key]
    [ForeignKey(nameof(Medicament))]
    public int IdMedicament { get; set; }
    
    [Key]
    [ForeignKey(nameof(Prescription))]
    public int IdPrescription { get; set; }

    public int Dose { get; set; }
    
    [MaxLength(100)]
    public string Details { get; set; } = null!;

    public Medicament Medicament { get; set; } = null!;
    public Prescription Prescription { get; set; } = null!;
}