using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestionStock.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Categorie = table.Column<string>(type: "TEXT", nullable: false),
                    Fournisseur = table.Column<string>(type: "TEXT", nullable: true),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    SeuilAlerte = table.Column<int>(type: "INTEGER", nullable: false),
                    Prix = table.Column<decimal>(type: "TEXT", nullable: false),
                    DateAjout = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Produits",
                columns: new[] { "Id", "Categorie", "DateAjout", "Description", "Fournisseur", "Nom", "Prix", "SeuilAlerte", "Stock" },
                values: new object[,]
                {
                    { 1, "Électronique", new DateTime(2026, 3, 23, 21, 45, 31, 384, DateTimeKind.Utc).AddTicks(776), null, "TechPro", "Câble USB-C", 9.99m, 10, 142 },
                    { 2, "Électronique", new DateTime(2026, 3, 23, 21, 45, 31, 384, DateTimeKind.Utc).AddTicks(783), null, "TechPro", "Hub HDMI", 29.99m, 5, 3 },
                    { 3, "Périphériques", new DateTime(2026, 3, 23, 21, 45, 31, 384, DateTimeKind.Utc).AddTicks(785), null, "Vision+", "Webcam HD", 49.99m, 5, 0 },
                    { 4, "Périphériques", new DateTime(2026, 3, 23, 21, 45, 31, 384, DateTimeKind.Utc).AddTicks(786), null, "Vision+", "Souris sans fil", 24.99m, 8, 38 },
                    { 5, "Périphériques", new DateTime(2026, 3, 23, 21, 45, 31, 384, DateTimeKind.Utc).AddTicks(788), null, "KeyMaster", "Clavier mécanique", 89.99m, 5, 12 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produits");
        }
    }
}
