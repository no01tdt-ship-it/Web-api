using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGenderToNullableInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Student",
                type: "int",
                nullable: true,
                comment: "Giới tính (1: Nam, 2: Nữ, 3: Khác, null: Chưa chọn)",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true,
                oldComment: "Giới tính (True: Nam, False: Nữ)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: true,
                comment: "Giới tính (True: Nam, False: Nữ)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Giới tính (1: Nam, 2: Nữ, 3: Khác, null: Chưa chọn)");
        }
    }
}
