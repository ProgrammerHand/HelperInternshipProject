using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helper.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EployeeReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Offers");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedTime",
                table: "Solutions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReservedEmployeeTime",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservedEmployeeTime", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservedEmployeeTime");

            migrationBuilder.DropColumn(
                name: "AssignedTime",
                table: "Solutions");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Offers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
