using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntittyToPortFolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Portfolios_PortFolioId",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_PortFolioId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "PortFolioId",
                table: "Experiences");

            migrationBuilder.AddColumn<int>(
                name: "PortFolioId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedDate",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedDate",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_PortFolioId",
                table: "Skills",
                column: "PortFolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Portfolios_PortFolioId",
                table: "Skills",
                column: "PortFolioId",
                principalTable: "Portfolios",
                principalColumn: "PortFolioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Portfolios_PortFolioId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_PortFolioId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "PortFolioId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Portfolios");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastUpdatedDate",
                table: "Portfolios",
                type: "date",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedDate",
                table: "Portfolios",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PortFolioId",
                table: "Experiences",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_PortFolioId",
                table: "Experiences",
                column: "PortFolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Portfolios_PortFolioId",
                table: "Experiences",
                column: "PortFolioId",
                principalTable: "Portfolios",
                principalColumn: "PortFolioId");
        }
    }
}
