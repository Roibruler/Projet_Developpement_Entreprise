using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrgenceTech.Migrations
{
    /// <inheritdoc />
    public partial class AjouterDateCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreation",
                table: "Utilisateurs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "DateCreation", "MotDePasse" },
                values: new object[] { new DateTime(2026, 5, 14, 22, 13, 28, 653, DateTimeKind.Local).AddTicks(2875), "$2a$11$w8j5zFfQT.956188WWkAM.UPNYsP2vfKxtXBSSRrkjtq2NVe64CVe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreation",
                table: "Utilisateurs");

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "ID",
                keyValue: 1,
                column: "MotDePasse",
                value: "$2a$11$I5vrX98gW7JZPzjW4GdxBuzCFr1dsXOf8JhLgiV05EkMtaJthFln6");
        }
    }
}
