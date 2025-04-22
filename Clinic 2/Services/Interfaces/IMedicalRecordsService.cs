public interface IMedicalRecordsService 
{
    int AddMedicalRecord(MedicalRecord entity);
    bool DeleteMedicalRecord(MedicalRecord entity);
    MedicalRecord GetMedicalRecordByID(int id);
    bool UpdateMedicalRecord(MedicalRecord entity);

}
