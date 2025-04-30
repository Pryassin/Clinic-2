public interface IPatientService
{
    int AddPaytient(Patient patient);
    bool DeletePatient(int id);
    Patient GetPatientById(int id);
    bool UpdatePatient(Patient patient);


}