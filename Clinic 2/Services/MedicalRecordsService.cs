using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class MedicalRecordsService : IMedicalRecordsService
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    public MedicalRecordsService(IMedicalRecordRepository medicalRecordRepository) 
    { _medicalRecordRepository = medicalRecordRepository; 
    }

}


