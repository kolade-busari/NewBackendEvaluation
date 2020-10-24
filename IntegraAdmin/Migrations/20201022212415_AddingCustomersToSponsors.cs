using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegraAdmin.Migrations
{
    public partial class AddingCustomersToSponsors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SponsorId",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_SponsorId",
                table: "Customers",
                column: "SponsorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Sponsors_SponsorId",
                table: "Customers",
                column: "SponsorId",
                principalTable: "Sponsors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Sponsors_SponsorId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_SponsorId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SponsorId",
                table: "Customers");
        }
    }
}
