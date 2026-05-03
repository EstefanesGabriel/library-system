using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.Data.Mappings;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Fine> Fines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            modelBuilder.ApplyConfiguration(new InstitutionMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new BookMap());
            modelBuilder.ApplyConfiguration(new LoanMap());
            modelBuilder.ApplyConfiguration(new FineMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}