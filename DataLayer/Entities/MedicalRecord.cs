public class MedicalRecord
{
    public int MedicalRecordID { get; set; }
    public string VisitDescription { get; set; }
    public string Diagnosis { get; set; }
    public string AdditionalNotes { get; set; }
    public Appointments Appointment { get; set; }
    public Prescription? Prescription { get; set; } // Navigation to Prescriptions
}
