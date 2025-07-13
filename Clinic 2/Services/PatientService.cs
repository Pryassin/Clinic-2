using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Service class for managing patient-related operations.
/// </summary>
public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PatientService"/> class.
    /// </summary>
    /// <param name="patientRepository">The patient repository dependency.</param>
    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    /// <summary>
    /// Ensures the provided patient object is not null.
    /// </summary>
    /// <param name="patient">The patient to check.</param>
    /// <exception cref="ArgumentNullException">Thrown if patient is null.</exception>
    void EnsurePatientNotNull(Patient patient)
    {
        if (patient == null)
            throw new ArgumentNullException(nameof(patient), "Patient cannot be null");
    }

    /// <summary>
    /// Adds a new patient to the repository.
    /// </summary>
    /// <param name="patient">The patient to add.</param>
    /// <returns>The ID of the newly added patient.</returns>
    /// <exception cref="ArgumentException">Thrown if the patient already exists.</exception>
    public int AddPaytient(Patient patient)
    {
        EnsurePatientNotNull(patient);

        return _patientRepository.Add(patient);
    }

    /// <summary>
    /// Deletes a patient by ID.
    /// </summary>
    /// <param name="id">The ID of the patient to delete.</param>
    /// <returns>True if the patient was deleted; otherwise, false.</returns>
    /// <exception cref="ArgumentException">Thrown if the ID is negative.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the patient does not exist.</exception>
    public bool DeletePatient(int id)
    {
        if (id < 0)
        {
            throw new ArgumentException("Invalid Patient ID can't be negative");
        }
        var patient = _patientRepository.GetByID(id);
        EnsurePatientNotNull(patient);

        return _patientRepository.Delete(patient);
    }

    /// <summary>
    /// Retrieves a patient by ID.
    /// </summary>
    /// <param name="id">The ID of the patient to retrieve.</param>
    /// <returns>The patient with the specified ID.</returns>
    /// <exception cref="ArgumentException">Thrown if the ID is negative.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the patient does not exist.</exception>
    public Patient GetPatientById(int id)
    {
        if (id < 0)
        {
            throw new ArgumentException("Invalid Patient ID can't be negative");
        }
        var patient = _patientRepository.GetByID(id);

        if(patient==null)
        {
            throw new KeyNotFoundException("Person was not found");
        }
        return patient;
    }

    /// <summary>
    /// Updates an existing patient in the repository.
    /// </summary>
    /// <param name="patient">The patient to update.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the patient is null.</exception>
    public bool UpdatePatient(Patient patient)
    {
        EnsurePatientNotNull(patient);

        try
        {
            return _patientRepository.Update(patient);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new KeyNotFoundException($"Patient with ID {patient.PatientID} not found");
        }
    }
}

