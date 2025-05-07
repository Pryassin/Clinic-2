public interface IPrescriptionsService
{
    Prescription? GetPrescriptionByMedicalRecordId(int id);
  
    Prescription? SearchPrescriptionByMedicationName(string name);
    Prescription? GetPrescriptionByPatientId(int patientId);

    public int AddPrescription(Prescription prescription);
    public bool UpdatePrescription(Prescription prescription);
    public Prescription GetPrescriptionById(int prescriptionId);
    public bool DeletePrescription(int prescriptionId);



}

