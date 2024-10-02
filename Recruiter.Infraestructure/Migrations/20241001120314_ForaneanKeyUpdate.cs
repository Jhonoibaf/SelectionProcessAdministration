using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recruiters.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ForaneanKeyUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
