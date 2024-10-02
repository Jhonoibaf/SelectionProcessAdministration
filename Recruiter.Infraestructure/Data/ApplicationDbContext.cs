using Microsoft.EntityFrameworkCore;
using Recruiters.Infraestructure.Configurations;
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

            modelBuilder.ApplyConfiguration(new CandidateConfiguration());
            modelBuilder.ApplyConfiguration(new CandidateExperienceConfiguration());
        }
    }
}
