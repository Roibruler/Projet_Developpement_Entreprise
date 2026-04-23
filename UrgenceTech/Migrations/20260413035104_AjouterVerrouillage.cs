using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrgenceTech.Migrations
{
    /// <inheritdoc />
    public partial class AjouterVerrouillage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateVerrouillage",
                table: "Utilisateurs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TentativesEchouees",
                table: "Utilisateurs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "DateVerrouillage", "TentativesEchouees" },
                values: new object[] { null, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateVerrouillage",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "TentativesEchouees",
                table: "Utilisateurs");
        }
    }
}
