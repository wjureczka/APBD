using APBD.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace APBD.DAL
{
    public partial class HospitalContext : DbContext
    {
        public HospitalContext()
        {
            
        }

        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {

        }

        public virtual DbSet<Medicament> Medicament { get; set; }
        
        public virtual DbSet<Prescription> Prescription { get; set; }

        public virtual DbSet<Doctor> Doctor { get; set; }
        
        public virtual DbSet<Patient> Patient { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament);

                entity.HasData(new Medicament
                {
                    IdMedicament = 1,
                    Name = "LEK",
                    Description = "LEKOWY",
                    Type = "DOUSTNY" 
                });

                entity.HasData(new Medicament
                {
                    IdMedicament = 2,
                    Name = "KEL",
                    Description = "YWOKEL",
                    Type = "YNTSUOD"
                });
            });
            
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor);

                entity.HasData(new Doctor
                {
                    IdDoctor = 1,
                    FirstName = "Lekarz",
                    LastName = "Lekarzowski",
                    Email = "lekarz.lekarzowski@gmail.com",
                });

                entity.HasData(new Doctor
                {
                    IdDoctor = 2,
                    FirstName = "Lekarz2",
                    LastName = "Lekarzowski2",
                    Email = "lekarz.lekarzowski2@gmail.com",
                });
            });
            
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient);

                entity.HasData(new Patient
                {
                    IdPatient = 1,
                    FirstName = "Adam",
                    LastName = "Adamowski",
                    BirthDate = DateTime.Now
                });

                entity.HasData(new Patient
                {
                    IdPatient = 2,
                    FirstName = "Adam2",
                    LastName = "Adamowski2",
                    BirthDate = DateTime.Now
                });
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription);

                entity.HasData(new Prescription
                {
                    IdPrescription = 1,
                    Date = DateTime.Now,
                    DueDate = DateTime.Now,
                    IdPatient = 1,
                    IdDoctor = 1,
                });

                entity.HasData(new Prescription
                {
                    IdPrescription = 2,
                    Date = DateTime.Now,
                    DueDate = DateTime.Now,
                    IdPatient = 2,
                    IdDoctor = 2
                });
            });

            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {

                entity.HasKey(e => new { e.IdPrescription, e.IdMedicament });

                entity.HasData(new PrescriptionMedicament
                { 
                    Details = "Szczegóły",
                    Dose = 1,
                    IdPrescription = 1,
                    IdMedicament = 1
                });

                entity.HasData(new PrescriptionMedicament
                {
                    Details = "Szczegóły",
                    Dose = 2,
                    IdPrescription = 2,
                    IdMedicament = 2
                });
            });
        }
    }
}