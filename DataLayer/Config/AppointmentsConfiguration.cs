using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AppointmentsConfiguration : IEntityTypeConfiguration<Appointments>
{
    void IEntityTypeConfiguration<Appointments>.Configure(EntityTypeBuilder<Appointments> builder)
    {
        builder.HasKey(e => e.AppointmentId);
        builder.Property(e => e.AppointmentId).ValueGeneratedOnAdd();
        builder.Property(e => e.AppointmentDateTime).HasColumnType("datetime");
        builder.Property(e => e.AppointmentStatus).HasColumnType("tinyint");

        builder.HasOne(e => e.Patient).WithMany(e => e.Appointments).HasForeignKey(e => e.PatientID).IsRequired().OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Doctor).WithMany(e => e.Appointments).HasForeignKey(e => e.DoctorID).IsRequired().OnDelete(DeleteBehavior.Restrict); ;
        builder.HasOne(e => e.Payments).WithOne(e => e.Appointments).HasForeignKey<Appointments>(e => e.PaymentID).IsRequired().OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.MedicalRecords).WithOne(e => e.Appointment).HasForeignKey<Appointments>(e => e.MedicalRecordID).IsRequired(false);
        builder.HasIndex(e => e.PaymentID).IsUnique();

        builder.ToTable("Appointments");

    }
}
