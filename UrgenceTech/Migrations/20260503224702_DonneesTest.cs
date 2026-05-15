using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UrgenceTech.Migrations
{
    /// <inheritdoc />
    public partial class DonneesTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Urgences",
                columns: new[] { "ID", "DateCreation", "Description", "Priorite", "Statut", "Titre", "UtilisateurID" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patient de 65 ans", "Critique", "Ouverte", "Patient en arrêt cardiaque", 1 },
                    { 2, new DateTime(2026, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patient de 25 ans", "Moyenne", "En cours", "Fracture du bras", 1 },
                    { 3, new DateTime(2026, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patient de 10 ans", "Haute", "Résolue", "Allergie alimentaire", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "ID",
                keyValue: 1,
                column: "MotDePasse",
                value: "$2a$11$/tdtO3tuFuyHRESnyv5nt.2LvVMotgUJ/bKg723svIPhFIb4CLdma");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Urgences",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Urgences",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Urgences",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "ID",
                keyValue: 1,
                column: "MotDePasse",
                value: "$2a$11$I5vrX98gW7JZPzjW4GdxBuzCFr1dsXOf8JhLgiV05EkMtaJthFln6");
        }
    }
}
