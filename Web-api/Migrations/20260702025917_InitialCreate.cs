using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchoolLevel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã cấp học")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Tên Cấp"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Mã định danh cấp"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Trạng thái hoạt động (True: Hoạt động, False: Khóa)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã trường học")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Tên trường học"),
                    SchoolLevelId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "Mã định danh trường"),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Nhập tỉnh"),
                    Ward = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Nhập Xã"),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "Nhập địa chỉ"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Trạng thái hoạt động (True: Hoạt động, False: Khóa)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.Id);
                    table.ForeignKey(
                        name: "FK_School_SchoolLevel_SchoolLevelId",
                        column: x => x.SchoolLevelId,
                        principalTable: "SchoolLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolClass",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã lớp học")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Tên lớp"),
                    Grade = table.Column<int>(type: "int", nullable: true, comment: "Khối lớp"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Mã định danh lớp"),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolClass_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã học sinh")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Họ và tên học sinh"),
                    SchoolClassId = table.Column<long>(type: "bigint", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Ảnh đại diện"),
                    Gender = table.Column<int>(type: "int", nullable: true, comment: "Giới tính (1: Nam, 2: Nữ, 3: Khác, null: Chưa chọn)"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Địa chỉ email"),
                    BirthDay = table.Column<DateTime>(type: "date", nullable: false, comment: "Ngày sinh"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Ngày tạo hồ sơ"),
                    CitizenId = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false, comment: "Số căn cước công dân"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false, comment: "Số điện thoại liên hệ"),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Tỉnh/Thành phố"),
                    Ward = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Phường/Xã"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Địa chỉ chi tiết"),
                    Course = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Khóa học"),
                    IsRetained = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Trạng thái lưu ban (True: Lưu ban, False: Không lưu ban)"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Trạng thái hoạt động (True: Hoạt động, False: Không hoạt động)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_SchoolClass_SchoolClassId",
                        column: x => x.SchoolClassId,
                        principalTable: "SchoolClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_School_Code",
                table: "School",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_School_SchoolLevelId",
                table: "School",
                column: "SchoolLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClass_Code",
                table: "SchoolClass",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClass_SchoolId",
                table: "SchoolClass",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolLevel_Code",
                table: "SchoolLevel",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SchoolClassId",
                table: "Student",
                column: "SchoolClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "SchoolClass");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "SchoolLevel");
        }
    }
}
