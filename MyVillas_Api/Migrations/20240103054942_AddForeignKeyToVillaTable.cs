using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyVillas_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaId",
                table: "villaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 3, 11, 19, 42, 123, DateTimeKind.Local).AddTicks(2649));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 3, 11, 19, 42, 123, DateTimeKind.Local).AddTicks(2798));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 3, 11, 19, 42, 123, DateTimeKind.Local).AddTicks(2801));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 3, 11, 19, 42, 123, DateTimeKind.Local).AddTicks(2803));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 3, 11, 19, 42, 123, DateTimeKind.Local).AddTicks(2805));

            migrationBuilder.CreateIndex(
                name: "IX_villaNumbers_VillaId",
                table: "villaNumbers",
                column: "VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_villaNumbers_villas_VillaId",
                table: "villaNumbers",
                column: "VillaId",
                principalTable: "villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_villaNumbers_villas_VillaId",
                table: "villaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_villaNumbers_VillaId",
                table: "villaNumbers");

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "villaNumbers");

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 3, 9, 56, 31, 382, DateTimeKind.Local).AddTicks(2832));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 3, 9, 56, 31, 382, DateTimeKind.Local).AddTicks(2846));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 3, 9, 56, 31, 382, DateTimeKind.Local).AddTicks(2847));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 3, 9, 56, 31, 382, DateTimeKind.Local).AddTicks(2849));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 3, 9, 56, 31, 382, DateTimeKind.Local).AddTicks(2851));
        }
    }
}
