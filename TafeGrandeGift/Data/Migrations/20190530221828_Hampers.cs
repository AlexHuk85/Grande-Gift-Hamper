using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TafeGrandeGift.Data.Migrations
{
    public partial class Hampers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HamperProduct_Hamper_HamperId",
                table: "HamperProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_HamperProduct_Product_ProductId",
                table: "HamperProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HamperProduct",
                table: "HamperProduct");

            migrationBuilder.RenameTable(
                name: "HamperProduct",
                newName: "hamperProducts");

            migrationBuilder.RenameIndex(
                name: "IX_HamperProduct_ProductId",
                table: "hamperProducts",
                newName: "IX_hamperProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_HamperProduct_HamperId",
                table: "hamperProducts",
                newName: "IX_hamperProducts_HamperId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hamperProducts",
                table: "hamperProducts",
                column: "HamperProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_hamperProducts_Hamper_HamperId",
                table: "hamperProducts",
                column: "HamperId",
                principalTable: "Hamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hamperProducts_Product_ProductId",
                table: "hamperProducts",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hamperProducts_Hamper_HamperId",
                table: "hamperProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_hamperProducts_Product_ProductId",
                table: "hamperProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hamperProducts",
                table: "hamperProducts");

            migrationBuilder.RenameTable(
                name: "hamperProducts",
                newName: "HamperProduct");

            migrationBuilder.RenameIndex(
                name: "IX_hamperProducts_ProductId",
                table: "HamperProduct",
                newName: "IX_HamperProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_hamperProducts_HamperId",
                table: "HamperProduct",
                newName: "IX_HamperProduct_HamperId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HamperProduct",
                table: "HamperProduct",
                column: "HamperProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_HamperProduct_Hamper_HamperId",
                table: "HamperProduct",
                column: "HamperId",
                principalTable: "Hamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HamperProduct_Product_ProductId",
                table: "HamperProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
