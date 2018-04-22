using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NomHadopi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    SuggestionID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AuthorEmail = table.Column<string>(nullable: true),
                    AuthorName = table.Column<string>(nullable: true),
                    DateSuggested = table.Column<DateTime>(nullable: false),
                    SuggestionValue = table.Column<string>(nullable: true),
                    UserIP = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.SuggestionID);
                });

            migrationBuilder.CreateTable(
                name: "UpVotes",
                columns: table => new
                {
                    UpVoteID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IpUser = table.Column<string>(nullable: true),
                    SuggestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpVotes", x => x.UpVoteID);
                    table.ForeignKey(
                        name: "FK_UpVotes_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "SuggestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_UserIP",
                table: "Suggestions",
                column: "UserIP");

            migrationBuilder.CreateIndex(
                name: "IX_UpVotes_SuggestionId",
                table: "UpVotes",
                column: "SuggestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpVotes");

            migrationBuilder.DropTable(
                name: "Suggestions");
        }
    }
}
