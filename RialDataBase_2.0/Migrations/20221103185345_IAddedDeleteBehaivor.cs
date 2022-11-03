using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RialDataBase_2._0.Migrations
{
    public partial class IAddedDeleteBehaivor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Promocodes_PromocodeId",
                table: "Bots");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Promocodes_PromocodeId",
                table: "Bots",
                column: "PromocodeId",
                principalTable: "Promocodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Promocodes_PromocodeId",
                table: "Bots");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Promocodes_PromocodeId",
                table: "Bots",
                column: "PromocodeId",
                principalTable: "Promocodes",
                principalColumn: "Id");
        }
    }
}
