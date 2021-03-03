namespace StudentsSocialMedia.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangesRelatedToGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_GroupId",
                table: "Posts",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_GroupId",
                table: "Images",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Groups_GroupId",
                table: "Images",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Groups_GroupId",
                table: "Posts",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Groups_GroupId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Groups_GroupId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_GroupId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Images_GroupId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Groups");
        }
    }
}
