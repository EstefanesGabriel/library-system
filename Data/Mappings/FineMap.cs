using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Data.Mappings
{
    public class FineMap : IEntityTypeConfiguration<Fine>
    {
        public void Configure(EntityTypeBuilder<Fine> builder)
        {
            // Tabela
            builder.ToTable("Fine");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Propriedades
            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnName("Amount")
                .HasColumnType("DECIMAL(5,2)");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.IsPaid)
                .IsRequired()
                .HasColumnName("IsPaid")
                .HasColumnType("BIT")
                .HasDefaultValue(false);

            builder.Property(x => x.PaidAt)
                .HasColumnName("PaidAt")
                .HasColumnType("SMALLDATETIME");

            builder.Property(x => x.LoanId)
                .IsRequired()
                .HasColumnName("LoanId")
                .HasColumnType("UNIQUEIDENTIFIER");
            
            builder.Property(x => x.InstitutionId)
               .IsRequired()
               .HasColumnName("InstitutionId")
               .HasColumnType("UNIQUEIDENTIFIER");

            builder.HasOne(x => x.Loan)
                .WithMany(x => x.Fines)
                .HasForeignKey(x => x.LoanId)
                .HasForeignKey("FK_Multa_Loan")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Loan)
                .WithMany(x => x.Fines)
                .HasForeignKey(x => x.LoanId)
                .HasForeignKey("FK_Multa_Loan")
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasIndex(x => x.IsPaid, "IX_Fine_IsPaid");
        }
    }
}