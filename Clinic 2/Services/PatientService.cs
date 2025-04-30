using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class PatientService : IPatientService
{
    IPatientRepository _patientRepository;
    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    void EnsurePatientNotNull(Patient patient)
    {
        if (patient == null)
            throw new ArgumentNullException(nameof(patient), "Patient cannot be null");
    }
    public int AddPaytient(Patient patient)
    {
        EnsurePatientNotNull(patient);
        if (_patientRepository.DoesExist(patient.PatientID))
        {
            throw new ArgumentException("Patient already exists");
        }
        return _patientRepository.Add(patient);
    }

    public bool DeletePatient(int id)
    {
        if(id <0)
        {
            throw new ArgumentException("Invalid Patient ID can't be negative");
        }
        var patient = _patientRepository.GetByID(id);
        EnsurePatientNotNull(patient);
             
        return _patientRepository.Delete(patient);
        
    }

    public Patient GetPatientById(int id)
    {
        if (id < 0)
        {
            throw new ArgumentException("Invalid Patient ID can't be negative");
        }
        var patient = _patientRepository.GetByID(id);
        EnsurePatientNotNull(patient);
        return patient;
    }

    public bool UpdatePatient(Patient patient)
    {
        EnsurePatientNotNull(patient);
        return _patientRepository.Update(patient);
        
    }
}

