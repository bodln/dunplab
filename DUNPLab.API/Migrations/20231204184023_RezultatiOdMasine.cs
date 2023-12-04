using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUNPLab.API.Migrations
{
    /// <inheritdoc />
    public partial class RezultatiOdMasine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RezultatiOdMasine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeIPrezimeTestera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodEpruvete = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumVreme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JesuLiPrebaceni = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezultatiOdMasine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupstanceRezultata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OznakaSupstance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vrednost = table.Column<double>(type: "float", nullable: false),
                    JeLiBiloGreske = table.Column<bool>(type: "bit", nullable: false),
                    IdRezultataOdMasine = table.Column<int>(type: "int", nullable: false),
                    RezultatOdMasineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupstanceRezultata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupstanceRezultata_RezultatiOdMasine_RezultatOdMasineId",
                        column: x => x.RezultatOdMasineId,
                        principalTable: "RezultatiOdMasine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupstanceRezultata_RezultatOdMasineId",
                table: "SupstanceRezultata",
                column: "RezultatOdMasineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupstanceRezultata");

            migrationBuilder.DropTable(
                name: "RezultatiOdMasine");
        }
    }
}
