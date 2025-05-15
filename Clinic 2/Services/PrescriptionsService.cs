using DataLayer.Repositories.Interfaces;

/// <summary>
/// Service class for managing prescriptions.
/// Provides methods to add, delete, retrieve, search, and update prescriptions.
/// </summary>
public class PrescriptionsService : IPrescriptionsService
{
    private readonly IPrescriptionsRepository _prescriptionsRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PrescriptionsService"/> class.
    /// </summary>
    /// <param name="prescriptionsRepository">The prescriptions repository dependency.</param>
    public PrescriptionsService(IPrescriptionsRepository prescriptionsRepository)
    {
        _prescriptionsRepository = prescriptionsRepository;
    }

    /// <summary>
    /// Adds a new prescription after validating its data.
    /// </summary>
    /// <param name="prescription">The prescription to add.</param>
    /// <returns>The ID of the newly added prescription.</returns>
    /// <exception cref="ArgumentNullException">Thrown if prescription is null.</exception>
    /// <exception cref="ArgumentException">Thrown if medication name is missing or dates are invalid.</exception>
    public int AddPrescription(Prescription prescription)
    {
        if (prescription == null)
            throw new ArgumentNullException(nameof(prescription));

        if (string.IsNullOrWhiteSpace(prescription.MedicationName))
            throw new ArgumentException("Medication name is required.");

        if (prescription.StartDate >= prescription.EndDate)
            throw new ArgumentException("Start date must be earlier than end date.");

        return _prescriptionsRepository.Add(prescription);
    }

    /// <summary>
    /// Deletes a prescription by its ID.
    /// </summary>
    /// <param name="prescriptionId">The ID of the prescription to delete.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    /// <exception cref="ArgumentException">Thrown if prescription ID is invalid.</exception>
    public bool DeletePrescription(int prescriptionId)
    {
        if (prescriptionId <= 0)
            throw new ArgumentException("Invalid prescription ID.");

        var prescription = _prescriptionsRepository.GetByID(prescriptionId);
        if (prescription == null)
            return false;

        return _prescriptionsRepository.Delete(prescription);
    }

    /// <summary>
    /// Retrieves a prescription by its ID.
    /// </summary>
    /// <param name="prescriptionId">The prescription ID.</param>
    /// <returns>The prescription if found.</returns>
    /// <exception cref="ArgumentException">Thrown if prescription ID is invalid.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if prescription is not found.</exception>
    public Prescription GetPrescriptionById(int prescriptionId)
    {
        if (prescriptionId <= 0)
            throw new ArgumentException("Invalid prescription ID.");

        var prescription = _prescriptionsRepository.GetByID(prescriptionId);
        if (prescription == null)
            throw new KeyNotFoundException("Prescription not found.");

        return prescription;
    }

    /// <summary>
    /// Retrieves a prescription by medical record ID.
    /// </summary>
    /// <param name="id">The medical record ID.</param>
    /// <returns>The prescription if found; otherwise, null.</returns>
    /// <exception cref="ArgumentException">Thrown if medical record ID is invalid.</exception>
    public Prescription? GetPrescriptionByMedicalRecordId(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid l record ID.");

        return _prescriptionsRepository.GetByMedicalRecordId(id);
    }

    /// <summary>
    /// Retrieves a prescription by patient ID.
    /// </summary>
    /// <param name="patientId">The patient ID.</param>
    /// <returns>The prescription if found; otherwise, null.</returns>
    /// <exception cref="ArgumentException">Thrown if patient ID is invalid.</exception>
    public Prescription? GetPrescriptionByPatientId(int patientId)
    {
        if (patientId <= 0)
            throw new ArgumentException("Invalid patient ID.");

        return _prescriptionsRepository.GetByPatientId(patientId);
    }

    /// <summary>
    /// Searches for a prescription by medication name.
    /// </summary>
    /// <param name="name">The medication name to search for.</param>
    /// <returns>The prescription if found; otherwise, null.</returns>
    /// <exception cref="ArgumentException">Thrown if medication name is missing.</exception>
    public Prescription? SearchPrescriptionByMedicationName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Medication name is required.");

        return _prescriptionsRepository.SearchByMedicationName(name);
    }

    /// <summary>
    /// Updates an existing prescription after validating its data.
    /// </summary>
    /// <param name="prescription">The prescription to update.</param>
    /// <returns>True if updated; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown if prescription is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if prescription does not exist.</exception>
    /// <exception cref="ArgumentException">Thrown if medication name is missing or dates are invalid.</exception>
    public bool UpdatePrescription(Prescription prescription)
    {
        if (prescription == null)
            throw new ArgumentNullException(nameof(prescription));

        if (!_prescriptionsRepository.DoesExist(prescription.PrescriptionID))
            throw new KeyNotFoundException("Prescription not found.");

        if (string.IsNullOrWhiteSpace(prescription.MedicationName))
            throw new ArgumentException("Medication name is required.");

        if (prescription.StartDate >= prescription.EndDate)
            throw new ArgumentException("Start date must be earlier than end date.");

        return _prescriptionsRepository.Update(prescription);
    }
}
