using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoltsegVetes_Razor.Migrations
{
    /// <inheritdoc />
    public partial class TablaLetrehozasa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoriak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nev = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Tipus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriak", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tranzakciok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KategoriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Osszeg = table.Column<decimal>(type: "TEXT", nullable: false),
                    Megjegyzes = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tranzakciok", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tranzakciok_Kategoriak_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategoriak",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tranzakciok_KategoriaId",
                table: "Tranzakciok",
                column: "KategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tranzakciok");

            migrationBuilder.DropTable(
                name: "Kategoriak");
        }
    }
}
