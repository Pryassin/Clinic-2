using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

/// <summary>
/// Service class for managing medical records.
/// Provides methods to add, delete, retrieve, and update medical records.
/// </summary>
public class MedicalRecordsService : IMedicalRecordsService
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="MedicalRecordsService"/> class.
    /// </summary>
    /// <param name="medicalRecordRepository">The repository for medical records.</param>
    public MedicalRecordsService(IMedicalRecordRepository medicalRecordRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
    }

    /// <summary>
    /// Adds a new medical record.
    /// </summary>
    /// <param name="medicalRecord">The medical record to add.</param>
    /// <returns>The ID of the added medical record.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="medicalRecord"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the medical record already exists.</exception>
    public int AddMedicalRecord(MedicalRecord medicalRecord)
    {
        if (medicalRecord == null)
        {
            throw new ArgumentNullException(nameof(medicalRecord), "can't be null");
        }
        if (_medicalRecordRepository.DoesExist(medicalRecord.MedicalRecordID))
        {
            throw new ArgumentException("Medical record already exists");
        }
        return _medicalRecordRepository.Add(medicalRecord);
    }

    /// <summary>
    /// Deletes an existing medical record.
    /// </summary>
    /// <param name="medicalRecord">The medical record to delete.</param>
    /// <returns>True if the record was deleted; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="medicalRecord"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the medical record does not exist.</exception>
    public bool DeleteMedicalRecord(MedicalRecord medicalRecord)
    {
        if (medicalRecord == null)
        {
            throw new ArgumentNullException(nameof(medicalRecord), "can't be null");
        }
        if (!_medicalRecordRepository.DoesExist(medicalRecord.MedicalRecordID))
        {
            throw new ArgumentException("Medical record does not exist");
        }
        return _medicalRecordRepository.Delete(medicalRecord);
    }

    /// <summary>
    /// Retrieves a medical record by its ID.
    /// </summary>
    /// <param name="id">The ID of the medical record.</param>
    /// <returns>The medical record with the specified ID.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="id"/> is negative.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the medical record is not found.</exception>
    public MedicalRecord GetMedicalRecordByID(int id)
    {
        if (id < 0)
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

    /// <summary>
    /// Updates an existing medical record.
    /// </summary>
    /// <param name="medicalRecord">The medical record to update.</param>
    /// <returns>True if the record was updated; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="medicalRecord"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the medical record does not exist.</exception>
    public bool UpdateMedicalRecord(MedicalRecord medicalRecord)
    {
        if (medicalRecord == null)
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


