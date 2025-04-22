using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class MedicalRecordsService : IMedicalRecordsService
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    public MedicalRecordsService(IMedicalRecordRepository medicalRecordRepository) 
    { _medicalRecordRepository = medicalRecordRepository; 
    }

    public int AddMedicalRecord(MedicalRecord medicalRecord)
    {
        if (medicalRecord == null)
        {
            throw new ArgumentNullException(nameof(medicalRecord),"can't be null");
        }
        if(_medicalRecordRepository.DoesExist(medicalRecord.MedicalRecordID))
        {
            throw new ArgumentException("Medical record already exists");
        }
        return _medicalRecordRepository.Add(medicalRecord);
    }

    public bool DeleteMedicalRecord(MedicalRecord medicalRecord)
    {
       if(medicalRecord == null)
        {
            throw new ArgumentNullException(nameof(medicalRecord),"can't be null");
        }
        if (!_medicalRecordRepository.DoesExist(medicalRecord.MedicalRecordID))
        {
            throw new ArgumentException("Medical record does not exist");
        }
        return _medicalRecordRepository.Delete(medicalRecord);
    }

    public MedicalRecord GetMedicalRecordByID(int id)
    {
        if(id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "ID can't be negative");
        }
        var medicalRecord = _medicalRecordRepository.GetByID(id);
        if (medicalRecord == null)
        {
            throw new KeyNotFoundException($"Medical record with ID {id} not found");
        }
        return medicalRecord;

    }

    public bool UpdateMedicalRecord(MedicalRecord medicalRecord)
    {
        if(medicalRecord == null)
        {
            throw new ArgumentNullException(nameof(medicalRecord), "can't be null");
        }
        if (!_medicalRecordRepository.DoesExist(medicalRecord.MedicalRecordID))
        {
            throw new ArgumentException("Medical record does not exist");
        }
        return _medicalRecordRepository.Update(medicalRecord);
    }
}


