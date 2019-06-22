using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TafeGrandeGift.Data.Migrations
{
    public partial class AddHamperFeedBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HamperFeedBack",
                columns: table => new
                {
                    HamperFeedBackId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HamperId = table.Column<int>(nullable: false),
                    UserFeedBack = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HamperFeedBack", x => x.HamperFeedBackId);
                    table.ForeignKey(
                        name: "FK_HamperFeedBack_Hamper_HamperId",
                        column: x => x.HamperId,
                        principalTable: "Hamper",
                        principalColumn: "HamperId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HamperFeedBack_HamperId",
                table: "HamperFeedBack",
                column: "HamperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HamperFeedBack");
        }
    }
}
