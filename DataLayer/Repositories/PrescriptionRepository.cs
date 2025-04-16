using DataLayer.Data;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionsRepository
    {
        private readonly ClinicDbContext _context;
        public PrescriptionRepository(ClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Prescription> GetActivePrescriptions(DateTime today)
        {
           return _context.Prescriptions.Where(p=>p.EndDate <= today);
        }

        public Prescription ? GetByMedicalRecordId(int id)
        {
            return _context.Prescriptions.Include(p => p.MedicalRecord).FirstOrDefault(p => p.MedicalRecordID == id);
        }
        public Prescription ? GetByPatientId(int patientId)
        {
            return _context.Prescriptions.FromSqlInterpolated($@"
             SELECT Pre.* 
             FROM dbo.Prescriptions AS Pre 
             INNER JOIN dbo.MedicalRecord Med ON Pre.MedicalRecordID = Med.MedicalRecordID 
             INNER JOIN dbo.Appointments App ON App.MedicalRecordID = Pre.MedicalRecordID
             WHERE App.PatientID = {patientId}").FirstOrDefault();
        }
        public Prescription ? SearchByMedicationName(string name)
        {
            return _context.Prescriptions.Include(p=>p.MedicalRecord).FirstOrDefault(P => P.MedicationName == name);
        }

     
    }
}
