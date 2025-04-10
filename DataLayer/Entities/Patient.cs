using System.ComponentModel.DataAnnotations;

public class Patient
{
    [Key]
    public int PatientID { get; set; }
    public int PersonID { set; get; } // Foreign key to Person
    public Person Person { get; set; } // Navigate to Person 
    public List<Appointments> Appointments { get; set; }
}
