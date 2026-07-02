using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_api.Migrations
{
    /// <inheritdoc />
    public partial class AddProvinceAndWardTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Province",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "School");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "School");

            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "Student",
                type: "bigint",
                nullable: true,
                comment: "Mã tỉnh/thành phố");

            migrationBuilder.AddColumn<long>(
                name: "WardId",
                table: "Student",
                type: "bigint",
                nullable: true,
                comment: "Mã phường/xã");

            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "School",
                type: "bigint",
                nullable: true,
                comment: "Mã tỉnh/thành phố");

            migrationBuilder.AddColumn<long>(
                name: "WardId",
                table: "School",
                type: "bigint",
                nullable: true,
                comment: "Mã phường/xã");

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tỉnh/thành phố")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Tên tỉnh/thành phố"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Mã định danh tỉnh/thành phố"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Trạng thái hoạt động (True: Hoạt động, False: Không hoạt động)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ward",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã phường/xã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Tên phường/xã"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Mã định danh phường/xã"),
                    ProvinceId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tỉnh/thành phố trực thuộc"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Trạng thái hoạt động (True: Hoạt động, False: Không hoạt động)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ward_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_ProvinceId",
                table: "Student",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_WardId",
                table: "Student",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_School_ProvinceId",
                table: "School",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_School_WardId",
                table: "School",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Province_Code",
                table: "Province",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ward_Code",
                table: "Ward",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ward_ProvinceId",
                table: "Ward",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_School_Province_ProvinceId",
                table: "School",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_School_Ward_WardId",
                table: "School",
                column: "WardId",
                principalTable: "Ward",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Province_ProvinceId",
                table: "Student",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Ward_WardId",
                table: "Student",
                column: "WardId",
                principalTable: "Ward",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_Province_ProvinceId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_School_Ward_WardId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Province_ProvinceId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Ward_WardId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Ward");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropIndex(
                name: "IX_Student_ProvinceId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_WardId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_School_ProvinceId",
                table: "School");

            migrationBuilder.DropIndex(
                name: "IX_School_WardId",
                table: "School");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "School");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "School");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Student",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "Tỉnh/Thành phố");

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "Student",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "Phường/Xã");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "School",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "Nhập tỉnh");

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "School",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "Nhập Xã");
        }
    }
}
