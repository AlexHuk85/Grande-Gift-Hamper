using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TafeGrandeGift.Data.Migrations
{
    public partial class addOrderHamper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hamper_Order_OrderId",
                table: "Hamper");

            migrationBuilder.DropIndex(
                name: "IX_Hamper_OrderId",
                table: "Hamper");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Hamper");

            migrationBuilder.CreateTable(
                name: "OrderHamper",
                columns: table => new
                {
                    OrderHamperId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HamperName = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    Qty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHamper", x => x.OrderHamperId);
                    table.ForeignKey(
                        name: "FK_OrderHamper_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHamper_OrderId",
                table: "OrderHamper",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderHamper");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Hamper",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hamper_OrderId",
                table: "Hamper",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hamper_Order_OrderId",
                table: "Hamper",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
