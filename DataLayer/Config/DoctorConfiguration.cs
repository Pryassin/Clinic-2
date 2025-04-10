using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(e => e.DoctorID);
        builder.Property(e => e.DoctorID).ValueGeneratedOnAdd();
        builder.Property(e => e.Specialization).HasMaxLength(100).IsRequired();
        builder.HasOne(e=>e.Person).WithOne().HasForeignKey<Doctor>(e=>e.PersonID).IsRequired(); // one to one relationship
        builder.HasIndex(e => e.PersonID).IsUnique(); // unique index to ensure the person is unique for each patient

        builder.ToTable("Doctors");
    }

}
