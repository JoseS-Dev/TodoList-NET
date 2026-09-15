using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList_NET.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFieldInTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Categories_CategoryId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Tasks",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_CategoryId",
                table: "Tasks",
                newName: "IX_Tasks_SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_SubCategories_SubCategoryId",
                table: "Tasks",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_SubCategories_SubCategoryId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "Tasks",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_SubCategoryId",
                table: "Tasks",
                newName: "IX_Tasks_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Categories_CategoryId",
                table: "Tasks",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
