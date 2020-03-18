using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Dataflow");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Vehicle",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Vehicle",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Vehicle",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Dataflow",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Dataflow",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Dataflow",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Dataflow",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Dataflow",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Dataflow",
                type: "datetime2",
                nullable: true);
        }
    }
}
