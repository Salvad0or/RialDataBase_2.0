using Microsoft.EntityFrameworkCore.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RialDataBase_2._0.Migrations
{
    public partial class PromocodeId_for_Bot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PromocodeId",
                table: "Bots",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bots_PromocodeId",
                table: "Bots",
                column: "PromocodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Promocodes_PromocodeId",
                table: "Bots",
                column: "PromocodeId",
                principalTable: "Promocodes",
                principalColumn: "Id"); 

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Promocodes_PromocodeId",
                table: "Bots");

            migrationBuilder.DropIndex(
                name: "IX_Bots_PromocodeId",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "PromocodeId",
                table: "Bots");
        }
    }
}
