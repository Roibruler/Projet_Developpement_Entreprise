using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrgenceTech.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Status = table.Column<bool>(type: "INTEGER", nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "ID", "Courriel", "MotDePasse", "NomComplet", "Role", "Status" },
                values: new object[] { 1, "admin@urgencetech.com", "admin123", "Admin Test", "Administrateur", true });
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
