using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrgenceTech.Migrations
{
    /// <inheritdoc />
    public partial class AjouterUrgences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Urgences",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titre = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Priorite = table.Column<string>(type: "TEXT", nullable: false),
                    Statut = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UtilisateurID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urgences", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Urgences_Utilisateurs_UtilisateurID",
                        column: x => x.UtilisateurID,
                        principalTable: "Utilisateurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "ID",
                keyValue: 1,
                column: "MotDePasse",
                value: "$2a$11$I5vrX98gW7JZPzjW4GdxBuzCFr1dsXOf8JhLgiV05EkMtaJthFln6");

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

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "ID",
                keyValue: 1,
                column: "MotDePasse",
                value: "$2a$11$.wa0BmRx22LSyp.mhLsvh.0d8OTKAkqsnmnKuDFXff3ajaXPpfE.G");
        }
    }
}
