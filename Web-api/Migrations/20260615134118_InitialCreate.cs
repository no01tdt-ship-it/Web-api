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
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã học sinh")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Họ và tên học sinh"),
                    ClassName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Tên lớp"),
                    Image = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Ảnh đại diện"),
                    Gender = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Giới tính (True: Nam, False: Nữ)"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Địa chỉ email"),
                    BirthDay = table.Column<DateTime>(type: "date", nullable: false, comment: "Ngày sinh"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Ngày tạo hồ sơ"),
                    CitizenId = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false, comment: "Số căn cước công dân"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false, comment: "Số điện thoại liên hệ"),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Tỉnh/Thành phố"),
                    Ward = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Phường/Xã"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Địa chỉ chi tiết"),
                    Course = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Khóa học"),
                    IsRetained = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Trạng thái lưu ban (True: Lưu ban, False: Không lưu ban)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
