using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.FullName)
                .IsRequired() 
                .HasColumnName("FullName")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired() 
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(x => x.PasswordHash)
                .IsRequired() 
                .HasColumnName("PasswordHash")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.NationalId)
                .IsRequired() 
                .HasColumnName("NationalId")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);

            builder.Property(x => x.ProfilePictureUrl)
                .IsRequired(false)
                .HasColumnName("ProfilePictureUrl")
                .HasColumnType("NVARCHAR(MAX)");

            builder.Property(x => x.Role)
                .IsRequired()
                .HasColumnName("Role")
                .HasColumnType("INT");

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
                .HasColumnName("InsitutionId")
                .HasColumnType("UNIQUEIDENTIFIER");

            builder.HasIndex(x => x.Email, "IX_User_Email")
                .IsUnique();

            builder.HasIndex(x => x.NationalId, "IX_User_NationalId")
                .IsUnique();

            builder.HasOne(x => x.Institution)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.InstitutionId);
        }
    }
}