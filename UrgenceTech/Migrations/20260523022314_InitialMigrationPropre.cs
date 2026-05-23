using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UrgenceTech.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationPropre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urgences_Utilisateurs_UtilisateurID",
                table: "Urgences");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMiseAJour",
                table: "Urgences",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TechnicienAssigneID",
                table: "Urgences",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "DateCreation", "MotDePasse" },
                values: new object[] { new DateTime(2026, 5, 22, 22, 23, 13, 944, DateTimeKind.Local).AddTicks(8030), "$2a$11$eIY47iOh03dp4Dr2uMg66OF4lVnD.6mIH136WaNXbvgYHfk/0vOoq" });

            migrationBuilder.CreateIndex(
                name: "IX_Urgences_TechnicienAssigneID",
                table: "Urgences",
                column: "TechnicienAssigneID");

            migrationBuilder.AddForeignKey(
                name: "FK_Urgences_Utilisateurs_TechnicienAssigneID",
                table: "Urgences",
                column: "TechnicienAssigneID",
                principalTable: "Utilisateurs",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Urgences_Utilisateurs_UtilisateurID",
                table: "Urgences",
                column: "UtilisateurID",
                principalTable: "Utilisateurs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urgences_Utilisateurs_TechnicienAssigneID",
                table: "Urgences");

            migrationBuilder.DropForeignKey(
                name: "FK_Urgences_Utilisateurs_UtilisateurID",
                table: "Urgences");

            migrationBuilder.DropIndex(
                name: "IX_Urgences_TechnicienAssigneID",
                table: "Urgences");

            migrationBuilder.DropColumn(
                name: "DateMiseAJour",
                table: "Urgences");

            migrationBuilder.DropColumn(
                name: "TechnicienAssigneID",
                table: "Urgences");

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
                columns: new[] { "DateCreation", "MotDePasse" },
                values: new object[] { new DateTime(2026, 5, 14, 22, 13, 28, 653, DateTimeKind.Local).AddTicks(2875), "$2a$11$/tdtO3tuFuyHRESnyv5nt.2LvVMotgUJ/bKg723svIPhFIb4CLdma" });

            migrationBuilder.AddForeignKey(
                name: "FK_Urgences_Utilisateurs_UtilisateurID",
                table: "Urgences",
                column: "UtilisateurID",
                principalTable: "Utilisateurs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
