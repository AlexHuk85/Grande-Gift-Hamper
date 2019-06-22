using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TafeGrandeGift.Data.Migrations
{
    public partial class addAddressID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_AspNetUsers_ApplicationUserId",
                table: "UserAddress");

            migrationBuilder.DropIndex(
                name: "IX_UserAddress_ApplicationUserId",
                table: "UserAddress");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "UserAddress",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "UserAddress",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_ApplicationUserId1",
                table: "UserAddress",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_AspNetUsers_ApplicationUserId1",
                table: "UserAddress",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_AspNetUsers_ApplicationUserId1",
                table: "UserAddress");

            migrationBuilder.DropIndex(
                name: "IX_UserAddress_ApplicationUserId1",
                table: "UserAddress");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "UserAddress");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserAddress",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_ApplicationUserId",
                table: "UserAddress",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_AspNetUsers_ApplicationUserId",
                table: "UserAddress",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
