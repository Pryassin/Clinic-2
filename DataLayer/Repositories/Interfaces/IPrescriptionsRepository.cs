namespace DataLayer.Repositories.Interfaces
{
    public interface IPrescriptionsRepository : IBaseRepository<Prescription>
    {
        Prescription ? GetByMedicalRecordId(int id);
        IQueryable<Prescription> GetActivePrescriptions(DateTime today);
        Prescription ? SearchByMedicationName(string name);
        Prescription ? GetByPatientId(int patientId);

    }
}
