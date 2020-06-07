using APBD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace APBD.DAL
{
    public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext()
        {
            
        }

        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
            
        }
        
        public virtual DbSet<Student> Student { get; set; }
        
        public virtual DbSet<Enrollment> Enrollment { get; set; }

        public virtual DbSet<Studies> Studies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s17082;Integrated Security=True");
                // .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IndexNumber);
            });
            
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.IdEnrollment);
            });
            
            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasKey(e => e.IdStudy);
            });
        }
    }
}