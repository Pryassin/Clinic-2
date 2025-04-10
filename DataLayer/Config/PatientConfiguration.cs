using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(e => e.PatientID);
        builder.Property(e => e.PatientID).ValueGeneratedOnAdd();
        builder.HasOne(e=>e.Person).WithOne().HasForeignKey<Patient>(e => e.PersonID).IsRequired();
        builder.HasIndex(e => e.PersonID).IsUnique();
        builder.ToTable("Patients");
    }
}