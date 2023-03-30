using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Migrations
{
    public partial class MoviesDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoviesInReq",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    searchToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imdbID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    processingTimeMS = table.Column<long>(type: "bigint", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ipAdress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesInReq", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviesInReq");
        }
    }
}
