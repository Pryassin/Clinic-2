using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.PersonID);
        builder.Property(p => p.PersonID).ValueGeneratedOnAdd();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.DateOfBirth).HasColumnType("date");
        builder.Property(p => p.Gender).IsRequired().HasColumnType("char");
        builder.Property(p => p.PhoneNumber).HasMaxLength(20).IsRequired(false);
        builder.Property(p => p.Email).HasMaxLength(100).IsRequired(false);
        builder.Property(p => p.Address).HasMaxLength(200).IsRequired(false);
        builder.ToTable("Persons");
    }
}
