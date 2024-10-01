using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recruiters.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class NameConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Candidates",
                newName: "birthdate");

            migrationBuilder.RenameColumn(
                name: "ModifyDate",
                table: "Candidates",
                newName: "modify_date");

            migrationBuilder.RenameColumn(
                name: "InsertDate",
                table: "Candidates",
                newName: "insert_date");

            migrationBuilder.RenameColumn(
                name: "ModifyDate",
                table: "CandidateExperiences",
                newName: "modify_date");

            migrationBuilder.RenameColumn(
                name: "InsertDate",
                table: "CandidateExperiences",
                newName: "insert_date");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "CandidateExperiences",
                newName: "end_date");

            migrationBuilder.RenameColumn(
                name: "BeginDate",
                table: "CandidateExperiences",
                newName: "bagin_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "birthdate",
                table: "Candidates",
                newName: "Birthdate");

            migrationBuilder.RenameColumn(
                name: "modify_date",
                table: "Candidates",
                newName: "ModifyDate");

            migrationBuilder.RenameColumn(
                name: "insert_date",
                table: "Candidates",
                newName: "InsertDate");

            migrationBuilder.RenameColumn(
                name: "modify_date",
                table: "CandidateExperiences",
                newName: "ModifyDate");

            migrationBuilder.RenameColumn(
                name: "insert_date",
                table: "CandidateExperiences",
                newName: "InsertDate");

            migrationBuilder.RenameColumn(
                name: "end_date",
                table: "CandidateExperiences",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "bagin_date",
                table: "CandidateExperiences",
                newName: "BeginDate");
        }
    }
}
