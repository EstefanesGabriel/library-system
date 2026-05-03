using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Data.Mappings
{
    public class InstitutionMap : IEntityTypeConfiguration<Institution>
    {
        public void Configure(EntityTypeBuilder<Institution> builder)
        {
            builder.ToTable("Institution");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.TaxId)
                .IsRequired()
                .HasColumnName("TaxId")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Address)
                .IsRequired()
                .HasColumnName("Address")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasColumnName("Phone")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);

            builder.Property(x => x.ContactEmail)
                .IsRequired()
                .HasColumnName("ContactEmail")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150);

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasColumnName("IsActive")
                .HasColumnType("BIT")
                .HasDefaultValue(true);
            
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}