
using System.ComponentModel.DataAnnotations.Schema;

public class MedicalReportGenerator
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MedicalReportId { get; set; }
    public MedicalRecord LastMedicalRecord { get; set; } // navigate to the last MedicalRecord
    public string DoctorName { get; set; }
    public string PatientName { get; set; }
    public string Diagnosis { get; set; }
    public Prescription? Prescriptions { get; set; } 


}
