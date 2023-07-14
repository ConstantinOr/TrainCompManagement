using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainCompManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainInformation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsQuantityAllowed = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainTreePath",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AncestorId = table.Column<long>(type: "bigint", nullable: true),
                    DescendantId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainTreePath", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainTreePath_TrainInformation_AncestorId",
                        column: x => x.AncestorId,
                        principalTable: "TrainInformation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainTreePath_TrainInformation_DescendantId",
                        column: x => x.DescendantId,
                        principalTable: "TrainInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainInformation_UniqueNumber",
                table: "TrainInformation",
                column: "UniqueNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainTreePath_AncestorId",
                table: "TrainTreePath",
                column: "AncestorId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainTreePath_DescendantId",
                table: "TrainTreePath",
                column: "DescendantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainTreePath");

            migrationBuilder.DropTable(
                name: "TrainInformation");
        }
    }
}
