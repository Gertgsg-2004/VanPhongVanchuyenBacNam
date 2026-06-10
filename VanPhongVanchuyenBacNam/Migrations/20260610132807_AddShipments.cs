using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VanPhongVanchuyenBacNam.Migrations
{
    /// <inheritdoc />
    public partial class AddShipments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackingCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SenderPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SenderAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RecipientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecipientPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RecipientAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RecipientProvince = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GoodsDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    ShippingFee = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CODAmount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TransportCompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_TransportCompanies_TransportCompanyId",
                        column: x => x.TransportCompanyId,
                        principalTable: "TransportCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_TrackingCode",
                table: "Shipments",
                column: "TrackingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_TransportCompanyId",
                table: "Shipments",
                column: "TransportCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shipments");
        }
    }
}
