using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PointOfSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCode = table.Column<int>(type: "int", nullable: false),
                    TillId = table.Column<int>(type: "int", nullable: false),
                    AdvanceWithdrawalTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashWithdrawalTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaleTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiveBackTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DrawerTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashPaymentTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditCardPaymentTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherPaymentTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiftCardPaymentTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointOfSales_Tills_TillId",
                        column: x => x.TillId,
                        principalTable: "Tills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointOfSales_Users_UserCode",
                        column: x => x.UserCode,
                        principalTable: "Users",
                        principalColumn: "UserCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tills",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ödeme Noktası 1" },
                    { 2, "Ödeme Noktası 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PointOfSales_TillId",
                table: "PointOfSales",
                column: "TillId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfSales_UserCode",
                table: "PointOfSales",
                column: "UserCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointOfSales");

            migrationBuilder.DropTable(
                name: "Tills");
        }
    }
}
