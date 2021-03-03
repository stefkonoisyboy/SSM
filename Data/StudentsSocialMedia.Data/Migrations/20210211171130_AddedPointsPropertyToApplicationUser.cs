namespace StudentsSocialMedia.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedPointsPropertyToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestPoints",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestPoints",
                table: "AspNetUsers");
        }
    }
}
