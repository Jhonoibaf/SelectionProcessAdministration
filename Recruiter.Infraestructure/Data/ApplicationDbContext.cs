using Microsoft.EntityFrameworkCore;
using Recruiters.Domain.Entities;

namespace Recruiters.Infraestructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Candidate> Candidates => Set<Candidate>();
        public DbSet<CandidateExperience> CandidateExperiences => Set<CandidateExperience>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>().HasKey(p => p.IdCandidate);
            modelBuilder.Entity<Candidate>().Property(p => p.Surname).HasMaxLength(150);
            modelBuilder.Entity<Candidate>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(p => p.Email).HasMaxLength(250);

            modelBuilder.Entity<CandidateExperience>().HasKey(p => p.IdCandidateExperience);
            modelBuilder.Entity<CandidateExperience>().Property(p => p.Company).HasMaxLength(100);
            modelBuilder.Entity<CandidateExperience>().Property(p => p.Job).HasMaxLength(100);
            modelBuilder.Entity<CandidateExperience>().Property(p => p.Description).HasMaxLength(4000);
            modelBuilder.Entity<CandidateExperience>().Property(p => p.Salary).HasPrecision(8,2);
        }

    }
}
