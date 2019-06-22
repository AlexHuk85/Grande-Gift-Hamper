using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TafeGrandeGift.Data.Migrations
{
    public partial class feedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HamperFeedBack_Hamper_HamperId",
                table: "HamperFeedBack");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HamperFeedBack",
                table: "HamperFeedBack");

            migrationBuilder.RenameTable(
                name: "HamperFeedBack",
                newName: "hamperFeedBacks");

            migrationBuilder.RenameIndex(
                name: "IX_HamperFeedBack_HamperId",
                table: "hamperFeedBacks",
                newName: "IX_hamperFeedBacks_HamperId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hamperFeedBacks",
                table: "hamperFeedBacks",
                column: "HamperFeedBackId");

            migrationBuilder.AddForeignKey(
                name: "FK_hamperFeedBacks_Hamper_HamperId",
                table: "hamperFeedBacks",
                column: "HamperId",
                principalTable: "Hamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hamperFeedBacks_Hamper_HamperId",
                table: "hamperFeedBacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hamperFeedBacks",
                table: "hamperFeedBacks");

            migrationBuilder.RenameTable(
                name: "hamperFeedBacks",
                newName: "HamperFeedBack");

            migrationBuilder.RenameIndex(
                name: "IX_hamperFeedBacks_HamperId",
                table: "HamperFeedBack",
                newName: "IX_HamperFeedBack_HamperId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HamperFeedBack",
                table: "HamperFeedBack",
                column: "HamperFeedBackId");

            migrationBuilder.AddForeignKey(
                name: "FK_HamperFeedBack_Hamper_HamperId",
                table: "HamperFeedBack",
                column: "HamperId",
                principalTable: "Hamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
