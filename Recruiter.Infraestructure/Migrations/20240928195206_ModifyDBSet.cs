using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recruiters.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDBSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateExperience_Candidate_IdCandidate",
                table: "CandidateExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateExperience",
                table: "CandidateExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidate",
                table: "Candidate");

            migrationBuilder.RenameTable(
                name: "CandidateExperience",
                newName: "CandidateExperiences");

            migrationBuilder.RenameTable(
                name: "Candidate",
                newName: "Candidates");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateExperience_IdCandidate",
                table: "CandidateExperiences",
                newName: "IX_CandidateExperiences_IdCandidate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateExperiences",
                table: "CandidateExperiences",
                column: "IdCandidateExperience");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates",
                column: "IdCandidate");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateExperiences_Candidates_IdCandidate",
                table: "CandidateExperiences",
                column: "IdCandidate",
                principalTable: "Candidates",
                principalColumn: "IdCandidate",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateExperiences_Candidates_IdCandidate",
                table: "CandidateExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateExperiences",
                table: "CandidateExperiences");

            migrationBuilder.RenameTable(
                name: "Candidates",
                newName: "Candidate");

            migrationBuilder.RenameTable(
                name: "CandidateExperiences",
                newName: "CandidateExperience");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateExperiences_IdCandidate",
                table: "CandidateExperience",
                newName: "IX_CandidateExperience_IdCandidate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidate",
                table: "Candidate",
                column: "IdCandidate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateExperience",
                table: "CandidateExperience",
                column: "IdCandidateExperience");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateExperience_Candidate_IdCandidate",
                table: "CandidateExperience",
                column: "IdCandidate",
                principalTable: "Candidate",
                principalColumn: "IdCandidate",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
