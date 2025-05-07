namespace DataLayer.Repositories.Interfaces
{
    public interface IPrescriptionsRepository : IBaseRepository<Prescription>
    {
        Prescription ? GetByMedicalRecordId(int id);
        Prescription ? SearchByMedicationName(string name);
     
        Prescription ? GetByPatientId(int patientId);



    }
}
