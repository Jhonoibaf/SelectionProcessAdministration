using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recruiters.Infraestructure.Models;

namespace Recruiters.Infraestructure.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<CandidateModel>
    {
        public void Configure(EntityTypeBuilder<CandidateModel> builder)
        {
            builder.HasKey(p => p.IdCandidate);

            builder.Property(p => p.IdCandidate)
                .HasColumnName("id_candidate");

            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            builder.Property(p => p.Surname)
                .HasMaxLength(150)
                .HasColumnName("surname");

            builder.Property(p => p.Birthdate)
                .HasColumnName("birthdate");

            builder.Property(p => p.Email)
                .HasMaxLength(250)
                .HasColumnName("email");

            builder.Property(p => p.InsertDate)
                .HasColumnName("insert_date"); 

            builder.Property(p => p.ModifyDate)
                .HasColumnName("modify_date")
                .IsRequired(false);

        }
    }
}
