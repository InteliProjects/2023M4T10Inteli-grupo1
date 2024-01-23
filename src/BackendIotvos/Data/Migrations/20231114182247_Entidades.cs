using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendIotvos.Data.Migrations
{
    public partial class Entidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConjuntoId",
                table: "Items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrdemServicoId",
                table: "Items",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Conjuntos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conjuntos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdensServico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdensServico", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ConjuntoId",
                table: "Items",
                column: "ConjuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrdemServicoId",
                table: "Items",
                column: "OrdemServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Conjuntos_ConjuntoId",
                table: "Items",
                column: "ConjuntoId",
                principalTable: "Conjuntos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_OrdensServico_OrdemServicoId",
                table: "Items",
                column: "OrdemServicoId",
                principalTable: "OrdensServico",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Conjuntos_ConjuntoId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_OrdensServico_OrdemServicoId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Conjuntos");

            migrationBuilder.DropTable(
                name: "OrdensServico");

            migrationBuilder.DropIndex(
                name: "IX_Items_ConjuntoId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OrdemServicoId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ConjuntoId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OrdemServicoId",
                table: "Items");
        }
    }
}
