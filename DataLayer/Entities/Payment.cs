﻿public class Payment
{
    public int PaymentID { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    public decimal AmountPaid { get; set; }
    public string AdditionalNotes { get; set; }

    public Appointments Appointments { get; set; } // Navigate To Appointments
}
