using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helper.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRelationInquiryOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Inquiries_PrecursorId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_PrecursorId",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "PrecursorId",
                table: "Offers",
                newName: "InquiryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InquiryId",
                table: "Offers",
                newName: "PrecursorId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PrecursorId",
                table: "Offers",
                column: "PrecursorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Inquiries_PrecursorId",
                table: "Offers",
                column: "PrecursorId",
                principalTable: "Inquiries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
