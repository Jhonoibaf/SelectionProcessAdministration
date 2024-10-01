using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recruiters.Infraestructure.Models;

namespace Recruiters.Infraestructure.Configurations
{
    public class CandidateExperienceConfiguration : IEntityTypeConfiguration<CandidateExperienceModel>
    {
        public void Configure(EntityTypeBuilder<CandidateExperienceModel> builder)
        {
            builder.HasKey(p => p.IdCandidateExperience);

            builder.Property(p => p.IdCandidateExperience)
                .HasColumnName("id_candidate_experience");

            builder.Property(p => p.Company)
                .HasMaxLength(100)
                .HasColumnName("company");
             
            builder.Property(p => p.Job)
                .HasMaxLength(100)
                .HasColumnName("job");
            
            builder.Property(p => p.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            
            builder.Property(p => p.Salary)
                .HasPrecision(8, 2)
                .HasColumnName("salary");
            
            builder.Property(p => p.BeginDate)
                .HasColumnName("bagin_date");
            
            builder.Property(p => p.EndDate)
                .HasColumnName("end_date")
                .IsRequired(false);
            
            builder.Property(p => p.InsertDate)
                .HasColumnName("insert_date");
            
            builder.Property(p => p.ModifyDate)
                .HasColumnName("modify_date")
                .IsRequired(false);
            
            builder.HasOne(p => p.Candidate)
               .WithMany()
               .HasForeignKey(p => p.IdCandidate)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
