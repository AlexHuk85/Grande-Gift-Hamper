using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TafeGrandeGift.Data.Migrations
{
    public partial class addHamperInHamperOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HamperId",
                table: "OrderHamper",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHamper_HamperId",
                table: "OrderHamper",
                column: "HamperId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHamper_Hamper_HamperId",
                table: "OrderHamper",
                column: "HamperId",
                principalTable: "Hamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHamper_Hamper_HamperId",
                table: "OrderHamper");

            migrationBuilder.DropIndex(
                name: "IX_OrderHamper_HamperId",
                table: "OrderHamper");

            migrationBuilder.DropColumn(
                name: "HamperId",
                table: "OrderHamper");
        }
    }
}
