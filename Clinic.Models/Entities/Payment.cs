using System.ComponentModel.DataAnnotations.Schema;

public class Payment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentID { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    public decimal AmountPaid { get; set; }
    public string AdditionalNotes { get; set; }

    public Appointments Appointments { get; set; } // Navigate To Appointments
}
