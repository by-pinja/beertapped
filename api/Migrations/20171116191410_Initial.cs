using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Abv = table.Column<string>(nullable: true),
                    American = table.Column<string>(nullable: true),
                    Ibu = table.Column<string>(nullable: true),
                    Imperial = table.Column<string>(nullable: true),
                    Ipa = table.Column<string>(nullable: true),
                    Lager = table.Column<string>(nullable: true),
                    Pale = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    Stout = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
