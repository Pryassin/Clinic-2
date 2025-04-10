using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => e.PaymentID);
        builder.Property(e => e.PaymentID).ValueGeneratedOnAdd();
        builder.Property(e => e.PaymentDate).HasColumnType("date");
        builder.Property(e => e.PaymentMethod).HasMaxLength(50).IsRequired();
        builder.Property(e => e.AmountPaid).HasColumnType("decimal").IsRequired();
        builder.Property(e => e.AdditionalNotes).HasMaxLength(200);
        builder.ToTable("Payments");
    }
}