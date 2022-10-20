using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RialDataBase_2._0.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Bots",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ClientId = table.Column<int>(type: "int", nullable: false),
            //        ChatId = table.Column<long>(type: "bigint", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Bots", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Bots_Client_ClientId",
            //            column: x => x.ClientId,
            //            principalTable: "Client",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Bots_ClientId",
            //    table: "Bots",
            //    column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bots");
        }
    }
}
