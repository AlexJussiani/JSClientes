using Microsoft.EntityFrameworkCore.Migrations;

namespace JS.Clientes.Infra.Migrations
{
    public partial class ClienteFornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EhCliente",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EhFornecedor",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EhCliente",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "EhFornecedor",
                table: "Clientes");
        }
    }
}
