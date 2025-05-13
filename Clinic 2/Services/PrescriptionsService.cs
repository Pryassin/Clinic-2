using DataLayer.Repositories.Interfaces;

public class PrescriptionsService : IPrescriptionsService
{
    private readonly IPrescriptionsRepository _prescriptionsRepository;
    public PrescriptionsService(IPrescriptionsRepository prescriptionsRepository)
    {
        _prescriptionsRepository = prescriptionsRepository ?? throw new ArgumentNullException(nameof(prescriptionsRepository));
    }


    int IPrescriptionsService.AddPrescription(Prescription prescription)
    {
        if (prescription == null)
            throw new ArgumentNullException(nameof(prescription));

        if (string.IsNullOrWhiteSpace(prescription.MedicationName))
            throw new ArgumentException("Medication name is required.");

        if (prescription.StartDate >= prescription.EndDate)
            throw new ArgumentException("Start date must be earlier than end date.");

        return _prescriptionsRepository.Add(prescription);
    }

    bool IPrescriptionsService.DeletePrescription(int prescriptionId)
    {
        if (prescriptionId <= 0)
            throw new ArgumentException("Invalid prescription ID.");

        var prescription = _prescriptionsRepository.GetByID(prescriptionId);
        if (prescription == null)
            return false;

        return _prescriptionsRepository.Delete(prescription);
    }



 

    Prescription IPrescriptionsService.GetPrescriptionById(int prescriptionId)
    {
        if (prescriptionId <= 0)
            throw new ArgumentException("Invalid prescription ID.");

        var prescription = _prescriptionsRepository.GetByID(prescriptionId);
        if (prescription == null)
            throw new KeyNotFoundException("Prescription not found.");

        return prescription;
    }

    Prescription? IPrescriptionsService.GetPrescriptionByMedicalRecordId(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid l record ID.");

        return _prescriptionsRepository.GetByMedicalRecordId(id);
    }

    Prescription? IPrescriptionsService.GetPrescriptionByPatientId(int patientId)
    {
        if (patientId <= 0)
            throw new ArgumentException("Invalid patient ID.");

        return _prescriptionsRepository.GetByPatientId(patientId);
    }

    Prescription? IPrescriptionsService.SearchPrescriptionByMedicationName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Medication name is required.");

        return _prescriptionsRepository.SearchByMedicationName(name);
    }

    bool IPrescriptionsService.UpdatePrescription(Prescription prescription)
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