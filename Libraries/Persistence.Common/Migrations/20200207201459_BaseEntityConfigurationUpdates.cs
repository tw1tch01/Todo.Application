using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Persistence.Common.Migrations
{
    public partial class BaseEntityConfigurationUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedProcess",
                table: "Notes",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1024) CHARACTER SET utf8mb4",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "Notes",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Notes",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256) CHARACTER SET utf8mb4",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedProcess",
                table: "Items",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1024) CHARACTER SET utf8mb4",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "Items",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Items",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256) CHARACTER SET utf8mb4",
                oldMaxLength: 256);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedProcess",
                table: "Notes",
                type: "varchar(1024) CHARACTER SET utf8mb4",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "Notes",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Notes",
                type: "varchar(256) CHARACTER SET utf8mb4",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedProcess",
                table: "Items",
                type: "varchar(1024) CHARACTER SET utf8mb4",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "Items",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Items",
                type: "varchar(256) CHARACTER SET utf8mb4",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
