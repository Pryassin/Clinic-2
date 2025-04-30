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

    public int AddPaytient(Patient patient)
    {
        if(patient == null)
        {
            throw new ArgumentNullException(nameof(patient),"can't be Null");
        }
        if(_patientRepository.DoesExist(patient.PatientID))
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
        if (patient == null)
        {
            throw new ArgumentException("Patient Does not Exist");
        }
        else
        {
            return _patientRepository.Delete(patient);
        }
    }

    public Patient GetPatientById(int id)
    {
        if (id < 0)
        {
            throw new ArgumentException("Invalid Patient ID can't be negative");
        }
        var patient = _patientRepository.GetByID(id);
        if (patient == null)
        {
            throw new ArgumentException("Patient not found");
        }
        return patient;
    }

    public bool UpdatePatient(Patient patient)
    {
        if(patient == null)
        {
            throw new ArgumentNullException(nameof(patient), "can't be Null");
        }
        
    }
}

