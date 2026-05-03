using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Data.Mappings
{
    public class LoanMap : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Loan");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.LoanDate)
                .IsRequired()
                .HasColumnName("LoanDate")
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.DueDate)
                .IsRequired()
                .HasColumnName("DueDate")
                .HasColumnType("SMALLDATETIME");

            builder.Property(x => x.ReturnedAt)
                .IsRequired(false)
                .HasColumnName("ReturnedAt")
                .HasColumnType("SMALLDATETIME");

            builder.Property(x => x.LoanStatus)
                .IsRequired()
                .HasColumnName("LoanStatus")
                .HasColumnType("INT");

            builder.Property(x => x.BookId)
                .IsRequired()
                .HasColumnName("BookId")
                .HasColumnType("UNIQUEIDENTIFIER");
            
            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("UNIQUEIDENTIFIER");

            builder.Property(x => x.InstitutionId)
                .IsRequired()
                .HasColumnName("InstitutionId")
                .HasColumnType("UNIQUEIDENTIFIER");

            builder.HasOne(x => x.Book)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.BookId)
                .HasConstraintName("FK_Loan_Book")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_Loan_User")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Institution)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.InstitutionId)
                .HasConstraintName("FK_Loan_Institution")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.LoanDate, "IX_Loan_LoanDate");                
            builder.HasIndex(x => x.LoanStatus, "IX_Loan_LoanStatus");                
        }
    }
}