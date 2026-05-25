using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrgenceTech.Migrations
{
    /// <inheritdoc />
    public partial class RecreateAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomComplet = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Courriel = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MotDePasse = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false),
                    TentativesEchouees = table.Column<int>(type: "INTEGER", nullable: false),
                    DateVerrouillage = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Urgences",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titre = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Statut = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Priorite = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateMiseAJour = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UtilisateurID = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicienAssigneID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urgences", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Urgences_Utilisateurs_TechnicienAssigneID",
                        column: x => x.TechnicienAssigneID,
                        principalTable: "Utilisateurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Urgences_Utilisateurs_UtilisateurID",
                        column: x => x.UtilisateurID,
                        principalTable: "Utilisateurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "ID", "Courriel", "DateCreation", "DateVerrouillage", "MotDePasse", "NomComplet", "Role", "Status", "TentativesEchouees" },
                values: new object[] { 1, "admin@urgencetech.com", new DateTime(2026, 5, 24, 3, 6, 45, 198, DateTimeKind.Local).AddTicks(9086), null, "$2a$11$N2CLmpYNCVCkmUBB9x3vgeDg0cozjVl03HbSyGRCKahlT1hlUGA6y", "Admin Test", "Administrateur", true, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Urgences_TechnicienAssigneID",
                table: "Urgences",
                column: "TechnicienAssigneID");

            migrationBuilder.CreateIndex(
                name: "IX_Urgences_UtilisateurID",
                table: "Urgences",
                column: "UtilisateurID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urgences");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
