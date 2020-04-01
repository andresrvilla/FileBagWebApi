using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileBagWebApi.DataAccess.EntityFramework.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "filebag");

            migrationBuilder.CreateTable(
                name: "Application",
                schema: "filebag",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 96, nullable: false),
                    URI = table.Column<string>(maxLength: 128, nullable: false),
                    Secret = table.Column<string>(maxLength: 128, nullable: false),
                    Token = table.Column<string>(maxLength: 128, nullable: true),
                    RowStatus = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                    table.UniqueConstraint("AK_Application_URI", x => x.URI);
                });

            migrationBuilder.CreateTable(
                name: "FileDetail",
                schema: "filebag",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Contents = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileElement",
                schema: "filebag",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    EntityType = table.Column<string>(maxLength: 96, nullable: false),
                    ContentLength = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 96, nullable: false),
                    MimeType = table.Column<string>(maxLength: 96, nullable: false),
                    FileDetailId = table.Column<Guid>(nullable: true),
                    Creator = table.Column<string>(maxLength: 32, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Modifier = table.Column<string>(maxLength: 32, nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    AccessCount = table.Column<long>(nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileElement_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "filebag",
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileElement_FileDetail_FileDetailId",
                        column: x => x.FileDetailId,
                        principalSchema: "filebag",
                        principalTable: "FileDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileElement_ApplicationId",
                schema: "filebag",
                table: "FileElement",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_FileElement_FileDetailId",
                schema: "filebag",
                table: "FileElement",
                column: "FileDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileElement",
                schema: "filebag");

            migrationBuilder.DropTable(
                name: "Application",
                schema: "filebag");

            migrationBuilder.DropTable(
                name: "FileDetail",
                schema: "filebag");
        }
    }
}
