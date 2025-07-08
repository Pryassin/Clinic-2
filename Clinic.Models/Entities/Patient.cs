using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Patient
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int PatientID { get; set; }
    public int PersonID { set; get; } // Foreign key to Person
    public Person Person { get; set; } // Navigate to Person 
    public string EmergencyContactName { get; set; }
    public string EmergencyContactPhone { get; set; }
    public List<Appointments> Appointments { get; set; }
}
