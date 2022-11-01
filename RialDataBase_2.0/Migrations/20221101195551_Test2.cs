using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RialDataBase_2._0.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Client_CientId",
                table: "Bots");

            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Promocodes_ClientId",
                table: "Bots");

            migrationBuilder.DropIndex(
                name: "IX_Bots_CientId",
                table: "Bots");


            migrationBuilder.DropColumn(
                name: "CientId",
                table: "Bots");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_PromocodeId",
                table: "Bots",
                column: "PromocodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Client_ClientId",
                table: "Bots",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Bots_Client_ClientId",
                table: "Bots");

            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Promocodes_PromocodeId",
                table: "Bots");

            migrationBuilder.DropIndex(
                name: "IX_Bots_PromocodeId",
                table: "Bots");

            migrationBuilder.AddColumn<int>(
                name: "CientId",
                table: "Bots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bots_CientId",
                table: "Bots",
                column: "CientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Client_CientId",
                table: "Bots",
                column: "CientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Promocodes_ClientId",
                table: "Bots",
                column: "ClientId",
                principalTable: "Promocodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
