using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PrescriptionsConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.HasKey(e => e.PrescriptionID);
        builder.Property(e => e.PrescriptionID).ValueGeneratedOnAdd();
        builder.Property(e => e.MedicationName).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Dosage).HasMaxLength(100).IsRequired();
        builder.Property(builder => builder.Frequency).HasMaxLength(50).IsRequired();
        builder.Property(e => e.StartDate).HasColumnType("date");   
        builder.Property(e => e.EndDate).HasColumnType("date");
        builder.Property(e => e.SpecialInstructions).HasMaxLength(200);

        builder.HasOne(e => e.MedicalRecord).WithOne(e=>e.Prescription).HasForeignKey<Prescription>(e => e.MedicalRecordID).IsRequired();
        builder.ToTable("Prescriptions");
    }
}

