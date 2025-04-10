using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.HasKey(e => e.MedicalRecordID);
        builder.Property(e => e.MedicalRecordID).ValueGeneratedOnAdd();
        builder.Property(e => e.VisitDescription).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Diagnosis).HasMaxLength(200).IsRequired();
        builder.Property(e => e.AdditionalNotes).HasMaxLength(200);

        builder.ToTable("MedicalRecord");
    }
}