using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AreaOrenk.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ilk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slaytlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResimYolu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Baslik = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Sira = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slaytlar", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Slaytlar",
                columns: new[] { "Id", "Aciklama", "Baslik", "ResimYolu", "Sira" },
                values: new object[,]
                {
                    { 1, "Lezzetli çilekli cupcake", "Çilekli Cupcake", "cilekliCup.jpg", 1 },
                    { 2, "Bol çikolatalı dondurma", "Çikolatalı Dondurma", "cikolataliDondurma.jpg", 2 },
                    { 3, "Yumuşacık pancake", "Pancake", "pancake.jpg", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slaytlar");
        }
    }
}
