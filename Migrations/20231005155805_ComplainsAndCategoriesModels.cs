using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace complainSystem.Migrations
{
    /// <inheritdoc />
    public partial class ComplainsAndCategoriesModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplainTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ComplainDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ComplainDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ComplainStatus = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complains_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Theft is the taking of another person's property or services without that person's permission or consent with the intent to deprive the rightful owner of it.", "Theft" },
                    { 2, "Harassment covers a wide range of behaviors of an offensive nature. It is commonly understood as behavior that demeans, humiliates or embarrasses a person, and it is characteristically identified by its unlikelihood in terms of social and moral reasonableness.", "Harrasment" },
                    { 3, "Assault is an act of inflicting physical harm or unwanted physical contact upon a person or, in some specific legal definitions, a threat or attempt to commit such an action.", "assult" }
                });

            migrationBuilder.InsertData(
                table: "Complains",
                columns: new[] { "Id", "CategoryId", "ComplainDateTime", "ComplainDescription", "ComplainStatus", "ComplainTitle" },
                values: new object[] { 1, 1, new DateTime(2023, 10, 5, 17, 58, 5, 758, DateTimeKind.Local).AddTicks(7925), "someone stole my bike", 1, "Theft" });

            migrationBuilder.CreateIndex(
                name: "IX_Complains_CategoryId",
                table: "Complains",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complains");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
