using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Persistence.MySQL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedProcess = table.Column<string>(maxLength: 1024, nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 256, nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifiedProcess = table.Column<string>(maxLength: 1024, nullable: false),
                    ParentItemId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: true),
                    StartedOn = table.Column<DateTime>(nullable: true),
                    CancelledOn = table.Column<DateTime>(nullable: true),
                    CompletedOn = table.Column<DateTime>(nullable: true),
                    ImportanceLevel = table.Column<string>(nullable: false),
                    PriorityLevel = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_Items_ParentItemId",
                        column: x => x.ParentItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedProcess = table.Column<string>(maxLength: 1024, nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 256, nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifiedProcess = table.Column<string>(maxLength: 1024, nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    ParentNoteId = table.Column<Guid>(nullable: true),
                    Comment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Notes_ParentNoteId",
                        column: x => x.ParentNoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ParentItemId",
                table: "Items",
                column: "ParentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ItemId",
                table: "Notes",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ParentNoteId",
                table: "Notes",
                column: "ParentNoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
