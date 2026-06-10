using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VanPhongVanchuyenBacNam.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransportCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BackupPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportCompanies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TransportCompanies",
                columns: new[] { "Id", "Address", "BackupPhone", "ContactPerson", "IsActive", "Name", "Note", "PhoneNumber", "Province" },
                values: new object[,]
                {
                    { 1, "Bến xe Vĩnh Niệm, Hải Phòng", "0912 245 245", "Anh Long", true, "Nhà xe Hoàng Long", "Nhận hàng cồng kềnh", "0225 392 0920", "Hải Phòng" },
                    { 2, "Bến xe Vinh, Nghệ An", null, "Chị Hoa", true, "Nhà xe Văn Minh", null, "0238 383 8383", "Nghệ An" },
                    { 3, "Bến xe Miền Đông, TP. Hồ Chí Minh", null, null, true, "Nhà xe Phương Trang", null, "0283 838 6852", "TP. Hồ Chí Minh" },
                    { 4, null, null, "Anh Tuấn", true, "Nhà xe Hải Âu", "Tuyến Hà Nội - Hải Phòng, nhiều chuyến mỗi ngày", "0225 374 7474", "Hải Phòng" },
                    { 5, "Bến xe phía Nam Nha Trang", null, null, true, "Nhà xe Quang Hạnh", null, "0258 381 2812", "Khánh Hòa" },
                    { 6, null, null, null, false, "Nhà xe Ba Miền", "Tạm ngừng hợp tác từ 5/2026", "0236 362 6262", "Đà Nẵng" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportCompanies");
        }
    }
}
