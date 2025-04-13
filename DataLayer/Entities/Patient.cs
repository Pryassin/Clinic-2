using System.ComponentModel.DataAnnotations;

public class Patient
{
  
    public int PatientID { get; set; }
    public int PersonID { set; get; } // Foreign key to Person
    public Person Person { get; set; } // Navigate to Person 
    public string EmergencyContactName { get; set; }
    public string EmergencyContactPhone { get; set; }
    public List<Appointments> Appointments { get; set; }
}
