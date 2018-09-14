using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KomunikatyRSO.Web.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WNSToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    PushChannel = table.Column<string>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    Dolnoslaskie = table.Column<bool>(nullable: false),
                    KujawskoPomorskie = table.Column<bool>(nullable: false),
                    Lubelskie = table.Column<bool>(nullable: false),
                    Lubuskie = table.Column<bool>(nullable: false),
                    Lodzkie = table.Column<bool>(nullable: false),
                    Malopolskie = table.Column<bool>(nullable: false),
                    Mazowieckie = table.Column<bool>(nullable: false),
                    Opolskie = table.Column<bool>(nullable: false),
                    Podkarpackie = table.Column<bool>(nullable: false),
                    Podlaskie = table.Column<bool>(nullable: false),
                    Pomorskie = table.Column<bool>(nullable: false),
                    Slaskie = table.Column<bool>(nullable: false),
                    Swietokrzyskie = table.Column<bool>(nullable: false),
                    WarminskoMazuskie = table.Column<bool>(nullable: false),
                    Wielkopolskie = table.Column<bool>(nullable: false),
                    Zachodniopomorskie = table.Column<bool>(nullable: false),
                    Ogolne = table.Column<bool>(nullable: false),
                    Meteo = table.Column<bool>(nullable: false),
                    Drogowe = table.Column<bool>(nullable: false),
                    Hydro = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
