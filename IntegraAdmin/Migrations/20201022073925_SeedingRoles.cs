using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegraAdmin.Migrations
{
    public partial class SeedingRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES (1, 'Admin', 'Admin')");
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES (2, 'Sponsor Read', 'Sponsor Read')");
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES (3, 'Sponsor Write', 'Sponsor Write')");
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES (4, 'Sponsor', 'Sponsor')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From AspNetRoles WHERE Id IN (1, 2, 3, 4)");
        }
    }
}
