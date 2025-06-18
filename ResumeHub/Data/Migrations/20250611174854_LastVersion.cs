using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class LastVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Portfolios_PortFolioId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Resumes_ResumeId",
                table: "Projects");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Resumes",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PortFolioId",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Portfolios_PortFolioId",
                table: "Projects",
                column: "PortFolioId",
                principalTable: "Portfolios",
                principalColumn: "PortFolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Resumes_ResumeId",
                table: "Projects",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "ResumeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Portfolios_PortFolioId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Resumes_ResumeId",
                table: "Projects");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Resumes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PortFolioId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Portfolios_PortFolioId",
                table: "Projects",
                column: "PortFolioId",
                principalTable: "Portfolios",
                principalColumn: "PortFolioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Resumes_ResumeId",
                table: "Projects",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "ResumeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
