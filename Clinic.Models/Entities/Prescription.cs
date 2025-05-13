public class Prescription
{
    public int PrescriptionID { get; set; }
    public int MedicalRecordID { get; set; }  // Foreign key to MedicalRecords
    public string MedicationName { get; set; }
    public string Dosage { get; set; }
    public string Frequency { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SpecialInstructions { get; set; }
    public MedicalRecord MedicalRecord { get; set; } // Navigation to MedicalRecords

}

