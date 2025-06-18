using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class Templates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResumeTemplateId",
                table: "Resumes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PortFolioTemplateId",
                table: "Portfolios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResumeTemplateId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "PortFolioTemplateId",
                table: "Portfolios");
        }
    }
}
