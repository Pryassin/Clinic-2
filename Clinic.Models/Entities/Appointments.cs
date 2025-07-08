using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Appointments
{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int AppointmentId { get; set; }
    public int PatientID { get; set; }
    public int DoctorID { get; set; } // foreign key to Doctor
    public DateTime AppointmentDateTime { get; set; }
    public enAppointmentStatus AppointmentStatus { get; set; }
    public int? MedicalRecordID { get; set; } // ? the Medical Record is optional 
    public int PaymentID { get; set; } 
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; } // Navigation to Doctor
    public Payment Payments { get; set; } 
    public MedicalRecord? MedicalRecords { get; set; }
}
