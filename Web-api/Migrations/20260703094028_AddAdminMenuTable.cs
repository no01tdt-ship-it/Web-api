using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_api.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminMenuTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Ward",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "Ngày tạo");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SchoolLevel",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "Ngày tạo");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SchoolClass",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "Ngày tạo");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "School",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "Ngày tạo");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Province",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "Ngày tạo");

            migrationBuilder.CreateTable(
                name: "AdminMenu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã menu")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true, comment: "Mã menu cha"),
                    MenuCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Mã chức năng menu"),
                    MenuName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Tên menu"),
                    Url = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "Đường dẫn menu"),
                    Icon = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "Icon menu"),
                    SortOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "Thứ tự hiển thị"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Trạng thái hoạt động"),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Menu hệ thống"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Ngày tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Ngày cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminMenu_AdminMenu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AdminMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminMenu_MenuCode",
                table: "AdminMenu",
                column: "MenuCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminMenu_ParentId",
                table: "AdminMenu",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminMenu");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Ward");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SchoolLevel");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SchoolClass");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "School");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Province");
        }
    }
}
