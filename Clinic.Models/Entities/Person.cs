using System.ComponentModel.DataAnnotations.Schema;

public class Person
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PersonId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public char Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }



}
