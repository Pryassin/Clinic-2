public class Doctor
{
    public int DoctorID { get; set; }
    public int PersonID { get; set; }
    public string Specialization { get; set; }
    public List<Appointments> Appointments { get; set; } // navigation to Appointment 
    public Person Person { get; set; }
}
