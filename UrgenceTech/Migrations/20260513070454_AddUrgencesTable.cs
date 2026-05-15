using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrgenceTech.Migrations
{
    
    public partial class AddUrgencesTable : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Urgences_TechnicienAssigneID",
                table: "Urgences",
                column: "TechnicienAssigneID");

            migrationBuilder.CreateIndex(
                name: "IX_Urgences_UtilisateurID",
                table: "Urgences",
                column: "UtilisateurID");
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urgences");
        }
    }
}
