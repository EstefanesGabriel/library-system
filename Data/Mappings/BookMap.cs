using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Data.Mappings
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // Tabela
            builder.ToTable("Book");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Propriedades
            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Author)
                .IsRequired()
                .HasColumnName("Author")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Isbn)
                .IsRequired()
                .HasColumnName("Isbn")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(13);

            builder.Property(x => x.Genre)
                .IsRequired()
                .HasColumnName("Genre")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            builder.Property(x => x.CoverImageUrl)
                .IsRequired(false)
                .HasColumnName("CoverImageUrl")
                .HasColumnType("NVARCHAR(MAX)");

            builder.Property(x => x.PublicationYear)
                .IsRequired()
                .HasColumnName("PublicationYear")
                .HasColumnType("INT");

            builder.Property(x => x.StockQuantity)
                .IsRequired()
                .HasColumnName("StockQuantity")
                .HasColumnType("INT")
                .HasDefaultValue(0);

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
            
            builder.Property(x => x.InstitutionId)
                .IsRequired()
                .HasColumnName("InstitutionId")
                .HasColumnType("UNIQUEIDENTIFIER");

            // Índices
            builder.HasIndex(x => x.Title, "IX_Book_Title");

            builder.HasIndex(x => x.Author, "IX_Book_Author");

            builder.HasIndex(x => x.Isbn, "IX_Book_Isbn")
                .IsUnique();

            builder.HasOne(x => x.Institution)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.InstitutionId);
        }
    }
}