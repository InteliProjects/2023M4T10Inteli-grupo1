using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendIotvos.Data.Migrations
{
    public partial class StatusItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Dropa a coluna antiga
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Items");

            // Cria a nova coluna com o tipo desejado
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Items",
                type: "integer",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Este método Down desfaz as alterações no caso de rollback da migração
            // Aqui, você pode recriar a coluna antiga se necessário, mas depende da sua lógica específica
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Items",
                type: "text",
                nullable: false);
        }
    }
}
