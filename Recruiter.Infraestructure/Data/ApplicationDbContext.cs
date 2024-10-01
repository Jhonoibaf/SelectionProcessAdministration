using Microsoft.EntityFrameworkCore;
using Recruiters.Infraestructure.Models;

namespace Recruiters.Infraestructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CandidateModel> Candidates => Set<CandidateModel>();
        public DbSet<CandidateExperienceModel> CandidateExperiences => Set<CandidateExperienceModel>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CandidateModel>().HasKey(p => p.IdCandidate);
            modelBuilder.Entity<CandidateModel>().Property(p => p.Surname).HasMaxLength(150);
            modelBuilder.Entity<CandidateModel>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<CandidateModel>().Property(p => p.Email).HasMaxLength(250);

            modelBuilder.Entity<CandidateExperienceModel>().HasKey(p => p.IdCandidateExperience);
            modelBuilder.Entity<CandidateExperienceModel>().Property(p => p.Company).HasMaxLength(100);
            modelBuilder.Entity<CandidateExperienceModel>().Property(p => p.Job).HasMaxLength(100);
            modelBuilder.Entity<CandidateExperienceModel>().Property(p => p.Description).HasMaxLength(4000);
            modelBuilder.Entity<CandidateExperienceModel>().Property(p => p.Salary).HasPrecision(8,2);
        }

    }
}
