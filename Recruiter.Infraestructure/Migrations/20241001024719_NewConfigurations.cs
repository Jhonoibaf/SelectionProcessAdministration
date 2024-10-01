using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recruiters.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class NewConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Candidates",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Candidates",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "IdCandidate",
                table: "Candidates",
                newName: "id_candidate");

            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "CandidateExperiences",
                newName: "salary");

            migrationBuilder.RenameColumn(
                name: "Job",
                table: "CandidateExperiences",
                newName: "job");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "CandidateExperiences",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "CandidateExperiences",
                newName: "company");

            migrationBuilder.RenameColumn(
                name: "IdCandidateExperience",
                table: "CandidateExperiences",
                newName: "id_candidate_experience");

            migrationBuilder.AddColumn<int>(
                name: "CandidateModelIdCandidate",
                table: "CandidateExperiences",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateExperiences_CandidateModelIdCandidate",
                table: "CandidateExperiences",
                column: "CandidateModelIdCandidate");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateExperiences_Candidates_CandidateModelIdCandidate",
                table: "CandidateExperiences",
                column: "CandidateModelIdCandidate",
                principalTable: "Candidates",
                principalColumn: "id_candidate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateExperiences_Candidates_CandidateModelIdCandidate",
                table: "CandidateExperiences");

            migrationBuilder.DropIndex(
                name: "IX_CandidateExperiences_CandidateModelIdCandidate",
                table: "CandidateExperiences");

            migrationBuilder.DropColumn(
                name: "CandidateModelIdCandidate",
                table: "CandidateExperiences");

            migrationBuilder.RenameColumn(
                name: "surname",
                table: "Candidates",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Candidates",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id_candidate",
                table: "Candidates",
                newName: "IdCandidate");

            migrationBuilder.RenameColumn(
                name: "salary",
                table: "CandidateExperiences",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "job",
                table: "CandidateExperiences",
                newName: "Job");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "CandidateExperiences",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "company",
                table: "CandidateExperiences",
                newName: "Company");

            migrationBuilder.RenameColumn(
                name: "id_candidate_experience",
                table: "CandidateExperiences",
                newName: "IdCandidateExperience");
        }
    }
}
